using RestaurantOrderApp.Domain.Enums.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestaurantOrderApp.Domain.Enums.Menu.DishType;
using static RestaurantOrderApp.Domain.Enums.Menu.TimeOfDay;

namespace RestaurantOrderApp.Domain.Entity
{
    public class DishesMenu
    {
        public DishesMenu(EDishType dishType, ETimeOfDay timeOfDay, string plateDescription, bool permitMultiple = false)
        {
            Id = Guid.NewGuid();
            DishType = dishType;
            TimeOfDay = timeOfDay;
            PlateDescription = plateDescription;
            PermitMultiple = permitMultiple;
        }

        public Guid Id { get; set; }
        public EDishType DishType { get; set; }
        public ETimeOfDay TimeOfDay { get; set; }
        public string PlateDescription { get; set; }
        public bool PermitMultiple { get; set; }
    }
}
