using RestaurantOrderApp.Domain.Entity;
using RestaurantOrderApp.Domain.Enums.Menu;
using RestaurantOrderApp.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Infra.Data.Repository.Menu
{
    public class MenuRepository : BaseRepository<DishesMenu>, IMenuRepository
    {
        RestauranteOrderAppContext _context;
        public MenuRepository(RestauranteOrderAppContext context) : base(context)
        {
            _context = context;
        }

        public List<DishesMenu> GetDishesByTimeOfDay(TimeOfDay.ETimeOfDay timeOfDay)
        {
            return _context.DishesMenu
                    .Where(dishesMenu => dishesMenu.TimeOfDay == timeOfDay)
                    .ToList();
        }
    }
}
