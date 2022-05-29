using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Notifications;
using RestaurantOrderApp.Domain.Interfaces.Application;
using System.Net;

namespace RestaurantOrderApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenuController : ApiController
    {
        private readonly IMenuApp _menuApp;

        public MenuController(INotificationHandler<DomainNotification> notifications,
            IMenuApp menuApp,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _menuApp = menuApp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        [HttpGet("GetMenuOptions")]
        public IActionResult Get(string menuRequest) => 
             Response(_menuApp.GetMenuOptions(menuRequest));
    }
}
