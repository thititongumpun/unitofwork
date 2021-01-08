using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Domain;

namespace src.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected AppDbContext Db { get; }
        protected DbSet<T> DbSet { get; }
        public BaseRepository(AppDbContext dbContext)
        {
            Db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = Db.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await DbSet
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual T Create(T model)
        {
            DbSet.Add(model);
            return model;
        }

        public virtual T Update(T model)
        {
            DbSet.Update(model);
            return model;
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(await DbSet.FindAsync(id));
        }
    }
}