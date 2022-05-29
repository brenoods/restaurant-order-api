using FluentValidation.Results;
using MediatR;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Notifications;
using RestaurantOrderApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Application.Application
{
    public class AppBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;

        public AppBase(IUnitOfWork uow, IMediatorHandler bus)
        {
            _uow = uow;
            _bus = bus;
        }
       
        protected void NotifyServerError(string message)
        {
            _bus.RaiseEvent(new DomainNotification("Exception", message));
        }

        protected void NotifyValidationErros(ValidationResult result)
        {
            foreach (var error in result.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }
    }
}
