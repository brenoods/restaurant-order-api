using RestaurantOrderApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestaurantOrderApp.Domain.Enums.Menu.TimeOfDay;

namespace RestaurantOrderApp.Domain.Interfaces.Repository
{
    public interface IMenuRepository: IBaseRepository<DishesMenu>
    {
        List<DishesMenu> GetDishesByTimeOfDay(ETimeOfDay timeOfDay);
    }
}
