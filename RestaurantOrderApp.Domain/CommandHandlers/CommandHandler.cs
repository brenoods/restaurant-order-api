using RestaurantOrderApp.Domain.Core.Notifications;
using MediatR;
using Newtonsoft.Json;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Commands;
using RestaurantOrderApp.Domain.Interfaces;

namespace RestaurantOrderApp.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyServerError(string message)
        {
            _bus.RaiseEvent(new DomainNotification("Exception", message));
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, JsonConvert.SerializeObject(new { field = error.PropertyName, message = error.ErrorMessage })));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "Houve um problema ao salvar os dados."));
            return false;
        }

    }
}
