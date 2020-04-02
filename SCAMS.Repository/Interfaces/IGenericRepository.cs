using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SCAMS.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Select(
                                Expression<Func<TEntity, bool>> filter = null,
                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                IList<Expression<Func<TEntity, object>>> includes = null,
                                int? page = null,
                                int? pageSize = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null, IList<Expression<Func<TEntity, object>>> includes = null);

        TEntity GetByID(object id);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria);

        Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity>> InsertAsync(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entity);

        Task<TEntity> GetByIDAsync(object id);
    }
}
