using RestaurantOrderApp.Domain.Core.Commands;
using RestaurantOrderApp.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task RaiseEvent<T>(T @event) where T : Event;
        Task RaiseEvent<T, TClass>(T @event, TClass @class) where T : Event where TClass : class;
    }
}
