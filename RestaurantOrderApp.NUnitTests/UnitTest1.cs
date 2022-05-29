using MediatR;
using NSubstitute;
using RestaurantOrderApp.Application.Application.Menu;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Notifications;
using RestaurantOrderApp.Domain.Entity;
using RestaurantOrderApp.Domain.Interfaces;
using RestaurantOrderApp.Domain.Interfaces.Application;
using RestaurantOrderApp.Domain.Interfaces.Repository;
using static RestaurantOrderApp.Domain.Enums.Menu.DishType;
using static RestaurantOrderApp.Domain.Enums.Menu.TimeOfDay;

namespace RestaurantOrderApp.NUnitTests
{
    public class Tests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IMenuRepository _menuRepository;

        public Tests()
        {
            _uow = Substitute.For<IUnitOfWork>();
            _bus = Substitute.For<IMediatorHandler>();
            _notifications = Substitute.For<INotificationHandler<DomainNotification>>();
            _menuRepository = Substitute.For<IMenuRepository>();

            var dishesMorningList = new List<DishesMenu>
            {
                new DishesMenu(EDishType.entree, ETimeOfDay.morning, "eggs"),
                new DishesMenu(EDishType.side, ETimeOfDay.morning, "toast"),
                new DishesMenu(EDishType.drink, ETimeOfDay.morning, "coffee", true)
            };

            var dishesNightList = new List<DishesMenu>
            {
                new DishesMenu(EDishType.entree, ETimeOfDay.night, "steak"),
                new DishesMenu(EDishType.side, ETimeOfDay.night, "potato", true),
                new DishesMenu(EDishType.drink, ETimeOfDay.night, "wine"),
                new DishesMenu(EDishType.dessert, ETimeOfDay.night, "cake")
            };

            _menuRepository.GetDishesByTimeOfDay(ETimeOfDay.morning).Returns(dishesMorningList);
            _menuRepository.GetDishesByTimeOfDay(ETimeOfDay.night).Returns(dishesNightList);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RequestInput_Morning_Entree_Side_And_Three_Coffees()
        {
            MenuApp menuApp = new MenuApp(_uow, _bus, _notifications, _menuRepository);
            string output = menuApp.GetMenuOptions("morning, 1, 2, 3, 3, 3");
            Console.WriteLine(output);
            Assert.That(output, Is.EqualTo("eggs, toast, coffee(x3)"));
        }

        [Test]
        public void Request_Invalid_Input()
        {
            MenuApp menuApp = new MenuApp(_uow, _bus, _notifications, _menuRepository);
            string output = menuApp.GetMenuOptions("1, 2, 3");
            Assert.That(output, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Request_Morning_Desert()
        {
            MenuApp menuApp = new MenuApp(_uow, _bus, _notifications, _menuRepository);
            string output = menuApp.GetMenuOptions("morning, 1, 4");
            Assert.That(output, Is.EqualTo("eggs, error"));
        }
    }
}