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
        /// Returns the menu plates
        /// </summary>
        /// <param name="menuRequest">Ex.: morning,1,2,3 or night,1,2,3</param>
        /// <returns></returns>
        /// <response code="200">Returns the menu plates for the selected options for morning or night</response>
        /// <response code="400">Returns the validations errors for the menuRequest</response>
        [HttpGet("GetMenuOptions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(string menuRequest) => 
             Response(_menuApp.GetMenuOptions(menuRequest));
    }
}
