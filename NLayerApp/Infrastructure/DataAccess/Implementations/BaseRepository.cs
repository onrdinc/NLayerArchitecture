using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Implementations
{
    public abstract class BaseRepository<TEntity, TContext, TId> : IBaseRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TContext : DbContext
    {
        protected readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            var entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public async Task AddRangeAsync(ICollection<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public void Delete(TEntity entity)
        {
             _dbSet.Remove(entity);
        }

        //public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList)
        //{
        //    throw new NotImplementedException();
        //}

        public IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includeList)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeList.Length > 0)
            {
                foreach (var include in includeList)
                {
                    query = query.Include(include);
                }
            }

            if (predicate == null)
            {
                return query;
            }

            return query.Where(predicate);
        }

        public async Task<TEntity> GetByIdAsync(TId id, params string[] includeList)
        {
            var query = _dbSet.AsQueryable();
            foreach (var include in includeList)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e=>e.Id.Equals(id));
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList)
        {
            var query = _dbSet.AsQueryable();
            if(includeList.Length > 0)
            {
                foreach (var include in includeList)
                {
                    query = query.Include(include);
                }
            }

            return await query.SingleOrDefaultAsync(predicate);
        }

        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _dbSet.Update(entity);
        }
    }
}
