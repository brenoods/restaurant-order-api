using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantOrderApp.Domain;
using RestaurantOrderApp.Domain.Entity;
using static RestaurantOrderApp.Domain.Enums.Menu.DishType;
using static RestaurantOrderApp.Domain.Enums.Menu.TimeOfDay;

namespace RestaurantOrderApp.Infra.Data
{
    public partial class RestauranteOrderAppContext : DbContext
    {
        public RestauranteOrderAppContext(DbContextOptions<RestauranteOrderAppContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<DishesMenu> DishesMenu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishesMenu>().HasData(
                new DishesMenu(EDishType.entree, ETimeOfDay.morning, "eggs"),
                new DishesMenu(EDishType.side, ETimeOfDay.morning, "toast"),
                new DishesMenu(EDishType.drink, ETimeOfDay.morning, "coffee", true),
                new DishesMenu(EDishType.entree, ETimeOfDay.night, "steak"),
                new DishesMenu(EDishType.side, ETimeOfDay.night, "potato", true),
                new DishesMenu(EDishType.drink, ETimeOfDay.night, "wine"),
                new DishesMenu(EDishType.dessert, ETimeOfDay.night, "cake"));
        }
    }
}