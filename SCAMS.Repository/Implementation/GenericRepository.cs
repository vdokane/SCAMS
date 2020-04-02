using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SCAMS.Repository.Interfaces;

namespace SCAMS.Repository.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Select(
                                 Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 IList<Expression<Func<TEntity, object>>> includes = null,
                                 int? page = null,
                                 int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != null && pageSize != null)
            {
                if (page.Value < 1) page = 1; //Otherwise, kaboom!

                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query;
        }

        /// <summary>
        /// This will simply get a count of records that match the criteria. This should be more effecient than bringing a result set back and then counting it.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null, IList<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includes != null)
            {
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await _dbSet.AnyAsync(criteria);
        }
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await _dbSet.FirstAsync(criteria);
        }


        public virtual async Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return await _dbSet.AddAsync(entity);
        }
        //Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity>

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// TODO, delete range!
        /// </summary>
        /// <param name="entityToDelete"></param>

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        //This needs to be tested! And added to interface
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        /*
        public virtual List<TEntity> StoredProcedureCall(GenericStoredProcedure sp)
        {
            //decimal dbug = -1;
            return _context.Database.SqlQuery<TEntity>(sp.SQLquery, sp.ParamList.ToArray()).ToList();
        }
        */

    }
}
