using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IBaseRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        Task<TEntity> GetByIdAsync(TId id, params string[] includeList);
        IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includeList);
        //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entity);
        void Delete(TEntity entity);
        void SoftDelete(TEntity entity);
        void Update(TEntity entity);

    }
}
