using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Notifications;

namespace RestaurantOrderApp.Domain.Core.Validator
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        private readonly IMediatorHandler _bus;

        public BaseValidator(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public ValidationResult ValidationResults { get; set; }

        protected void NotifyValidationErrors()
        {
            foreach (var error in ValidationResults.Errors)
            {
                _bus.RaiseEvent(new DomainNotification("Validacao", JsonConvert.SerializeObject(new { field = error.PropertyName, message = error.ErrorMessage })));
            }
        }

    }
}
