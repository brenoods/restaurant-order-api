using System.Threading.Tasks;
using MediatR;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Commands;
using RestaurantOrderApp.Domain.Core.Events;

namespace RestaurantOrderApp.Infra.Bus
{
    public sealed class InMemoryBus: IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }

        public Task RaiseEvent<T, TClass>(T @event, TClass @class) where T : Event where TClass : class
        {
            return _mediator.Publish(@event);
        }
    }
}
