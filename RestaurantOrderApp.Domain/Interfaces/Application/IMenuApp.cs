using RestaurantOrderApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Domain.Interfaces.Application
{
    public interface IMenuApp
    {
        string GetMenuOptions(string menuRequest);
    }
}
