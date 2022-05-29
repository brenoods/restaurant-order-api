using RestaurantOrderApp.Domain.Interfaces;

namespace RestaurantOrderApp.Infra.Data.UoW
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly RestauranteOrderAppContext _context;

        public UnitOfWork(RestauranteOrderAppContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
