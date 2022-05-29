using MediatR;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Notifications;
using RestaurantOrderApp.Domain.Entity;
using RestaurantOrderApp.Domain.Interfaces;
using RestaurantOrderApp.Domain.Interfaces.Application;
using RestaurantOrderApp.Domain.Interfaces.Repository;
using RestaurantOrderApp.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestaurantOrderApp.Domain.Enums.Menu.DishType;
using static RestaurantOrderApp.Domain.Enums.Menu.TimeOfDay;

namespace RestaurantOrderApp.Application.Application.Menu
{
    public class MenuApp : AppBase, IMenuApp
    {
        private readonly IMenuRepository _menuRepository;
        IMediatorHandler _bus;

        public MenuApp(IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IMenuRepository menuRepository) : base(uow, bus)
        {
            _menuRepository = menuRepository;
            _bus = bus;
        }

        public string GetMenuOptions(string menuRequest)
        {
            var validator = new MenuRequestValidator(_bus);

            var menuRequestString = menuRequest.ToLower().Replace(" ", "");

            validator.ValidateMenuInputRequest(menuRequestString);

            if (validator.ValidationResults.IsValid)
            {
                var menuRequestOptions = menuRequestString.Split(",");

                var timeOfDayString = menuRequestOptions.Take(1).FirstOrDefault();
                var dishesSelectedOptions = menuRequestOptions.Skip(1)
                                        .Select(dishOption => Convert.ToInt32(dishOption))
                                        .OrderBy(dishOption => dishOption)
                                        .ToList();

                ETimeOfDay? timeOfDay = GetTimeOfDayEnumByString(timeOfDayString);
                if (timeOfDay != null)
                {
                    if (dishesSelectedOptions.Count > 0)
                    {
                        List<string> dishesOptionsSelected = new();
                        var dishesByTimeOfDay = _menuRepository.GetDishesByTimeOfDay(timeOfDay.Value).ToList();

                        foreach (EDishType dishOption in dishesSelectedOptions)
                        {
                            DishesMenu? dish = dishesByTimeOfDay.FirstOrDefault(dishes => dishes.DishType == dishOption);
                            if (dish == null || dishesOptionsSelected.Contains(dish.PlateDescription) && !dish.PermitMultiple)
                            {
                                dishesOptionsSelected.Add("error");
                                break;
                            }
                            else
                                dishesOptionsSelected.Add(dish.PlateDescription);

                        }
                        var dishesOptionsSelectedGroup = dishesOptionsSelected.
                                                            GroupBy(group => group)
                                                            .Select(dishesGrouped => $"{dishesGrouped.Key}{(dishesGrouped.Count() > 1 ? $"(x{dishesGrouped.Count()})" : "")}");
                        return $"{string.Join(", ", dishesOptionsSelectedGroup.Select(dishes => dishes))}";
                    }
                        _bus.RaiseEvent(new DomainNotification("Not Found", "Select at least one Dish Type"));
                }
                else
                    _bus.RaiseEvent(new DomainNotification("Not Found", "Time of Day not found"));
            }

            return string.Empty;
        }
    }
}
