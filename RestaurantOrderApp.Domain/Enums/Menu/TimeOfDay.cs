using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Domain.Enums.Menu
{
    public class TimeOfDay
    {
        public enum ETimeOfDay
        {
            morning,
            night
        }

        public static ETimeOfDay? GetTimeOfDayEnumByString(string timeOfDayString)
        {
            Enum.TryParse(typeof(ETimeOfDay), timeOfDayString, out var timeOfDay);
            return (ETimeOfDay?)timeOfDay;
        }
    }
}
