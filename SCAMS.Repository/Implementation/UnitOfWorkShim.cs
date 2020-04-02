using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCAMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SCAMS.Repository.Context;


namespace SCAMS.Repository.Implementation
{
    public class UnitOfWorkShim : IDisposable, IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private bool _disposed = false;
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        /// <summary>
        /// Use this constructor for working on with the SCEM's database
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public UnitOfWorkShim(DbContextOptionsBuilder<ScamsDBContext> optionsBuilder)
        {
            _dbContext = new ScamsDBContext(optionsBuilder.Options);
        }

        /// <summary>
        /// A more generic contstructor to work with any database or an additional one. 
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWorkShim(DbContext dbContext)
        {
            _dbContext = dbContext;  //Use this to use either PFDWH or HumanResources.
        }


        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }
            IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repo);
            return repo;
        }


        #region transaction methods
        public async Task<int> SaveAsync()
        {
            /* With these you can do last minute checking or add row level auditing
            var addedAuditedEntities = _dbContext.ChangeTracker.Entries()
                                        .Where(p => p.State == EntityState.Added)
                                        .Select(p => p.Entity);

            var modifiedAuditedEntities = _dbContext.ChangeTracker.Entries()
                                            .Where(p => p.State == EntityState.Modified)
                                            .Select(p => p.Entity);
                                            */

            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This must be used in a Using statement, otherwise the connection will remain open!
        /// </summary>
        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();  //Dont return anything. Keep the DB transaction context in the repo  level. 
        }
        /// <summary>
        /// This is what makes the Shim a shim!
        /// </summary>
        public void Commit()
        {
            Rollback();
        }


        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();
        }

        #endregion

        #region IDisposable requirements
        /// <summary>
        /// In case this is used in a Using statement.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
