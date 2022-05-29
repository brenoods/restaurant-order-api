using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Domain.Core.Events
{
    public class Event : Message, INotification
    {
        public DateTime TimeStamp { get; private set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }

        public Event(string eventName)
        {
            MessageType = eventName;
            TimeStamp = DateTime.Now;
        }
    }
}
