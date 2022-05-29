using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Domain.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        void Add(TEntity entity);

        void Add(List<TEntity> entities);

        void Delete(object id);

        void Remove(TEntity entityToRemove);

        void Update(TEntity entityToUpdate);
    }
}
