using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using src.Domain;

namespace src.Persistence.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetById(Guid id);

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate);

        T Create(T entity);

        T Update(T entity);

        Task Delete(Guid id);
    }
}