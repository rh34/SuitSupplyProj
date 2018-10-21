using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SS.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        bool Save();
    }
}