using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantOrderApp.Application.Application.Menu;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Notifications;
using RestaurantOrderApp.Domain.Interfaces;
using RestaurantOrderApp.Domain.Interfaces.Application;
using RestaurantOrderApp.Domain.Interfaces.Repository;
using RestaurantOrderApp.Infra.Bus;
using RestaurantOrderApp.Infra.Data;
using RestaurantOrderApp.Infra.Data.Repository.Menu;
using RestaurantOrderApp.Infra.Data.UoW;

namespace RestaurantOrderApp.Infra.DependencyInjection
{
    public class DependencyInjectionBootstrapper
    {
        public static void RegisterServices(IServiceCollection services) {
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<RestauranteOrderAppContext>(opt => opt.UseInMemoryDatabase("RestaurantDatabase"));
            services.AddScoped<RestauranteOrderAppContext>();      

            services.AddScoped<IMenuApp, MenuApp>();
            services.AddScoped<IMenuRepository, MenuRepository>();
        }
    }
}
