using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCAMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SCAMS.Repository.Context;

namespace SCAMS.Repository.Implementation
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public string LoginName { get; set; } //Only use this is you are saving the userID or user name for auditing reason
        //public string AppName { get; set; }

        //Of course: https://docs.microsoft.com/en-us/ef/core/saving/transactions it has to be different
        //private DbContextTransaction _transaction;


        public UnitOfWork(DbContextOptionsBuilder<ScamsDBContext> optionsBuilder)
        {

            //SO here is where the connection string needs to be injected:
            //DbContextOptionsBuilder<MoneyDBContext> optionsBuilder = new DbContextOptionsBuilder<MoneyDBContext>();
            //optionsBuilder.u
            //optionsBuilder.UseSqlServer(@"server=DESKTOP-DG5NGVA\SQLEXPRESS;database=Money;integrated security=true;");

            _dbContext = new ScamsDBContext(optionsBuilder.Options);
            //_transactionCount = 0;
            //_transaction = null;
        }

        //VDO testing to make this more generic so ANY db can be used. 
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;  //Use this to use either PFDWH or HumanResources.
            //_transactionCount = 0;
            //_transaction = null;
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

        public async Task<int> SaveAsync()
        {
            var addedAuditedEntities = _dbContext.ChangeTracker.Entries()
                                        .Where(p => p.State == EntityState.Added)
                                        .Select(p => p.Entity);

            var modifiedAuditedEntities = _dbContext.ChangeTracker.Entries()
                                            .Where(p => p.State == EntityState.Modified)
                                            .Select(p => p.Entity);

            // Auditing will be done differently in this project VDO 2019/04/07
            //foreach (var added in addedAuditedEntities)
            //{
            //  if (_dbContext.Entry(added).Property("CreateUser") != null)
            //{
            //  _dbContext.Entry(added).Property("CreateUser").CurrentValue = LoginName;
            //}
            //if (_dbContext.Entry(added).Property("CreateTime") != null)
            //{
            //  _dbContext.Entry(added).Property("CreateTime").CurrentValue = DateTime.Now;
            //}

            //if (_dbContext.Entry(added).Property("CreateApp") != null)
            //{
            //  _dbContext.Entry(added).Property("CreateApp").CurrentValue = AppName;
            //}
            //}

            //foreach (var modified in modifiedAuditedEntities)
            //{
            //  if (_dbContext.Entry(modified).Property("UpdateUser") != null)
            //                {
            //                  _dbContext.Entry(modified).Property("UpdateUser").CurrentValue = LoginName;
            //            }

            //          if (_dbContext.Entry(modified).Property("UpdateTime") != null)
            //        {
            //          _dbContext.Entry(modified).Property("UpdateTime").CurrentValue = DateTime.Now;
            //    }

            //  if (_dbContext.Entry(modified).Property("UpdateApp") != null)
            //{
            //  _dbContext.Entry(modified).Property("UpdateApp").CurrentValue = AppName;
            //}
            //} 

            return await _dbContext.SaveChangesAsync();
        }

        private bool _disposed = false;

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
        /// <summary>
        /// THIS MUST BE USED IN A USING STATEMENT
        /// </summary>
        /// <returns></returns>
        public async Task BeginTransactionAsync()
        {



            /* For Isolation Levels:
             * So if you have using Microsoft.EntityFrameworkCore; and don't see it, add reference to the Microsoft.EntityFrameworkCore.Relational.dll assembly / package.*/
            await _dbContext.Database.BeginTransactionAsync();

            //_dbContext.Database.BeginTransaction();
            /*
            _dbContext.Database.co
            _transactionCount++;
            if (_transaction == null)
            {
                if (_dbContext.Database.Connection.State == System.Data.ConnectionState.Closed)
                    _dbContext.Database.Connection.Open();

                _transaction = _dbContext.Database.BeginTransaction();
            }

            return _transaction;
            */
        }
        /* Transaction doesn't have an isolation level?
        public async Task BeginTransactionDirtyAsync()
        {
            //https://apisof.net/catalog/Microsoft.EntityFrameworkCore.Storage.IRelationalTransactionManager.BeginTransactionAsync(IsolationLevel,CancellationToken)
            await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted);
            //await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
        } */

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
            /*
            _transactionCount--;
            if (_transactionCount == 0)
            {
                _transaction.Commit();
                _transaction = null;

                //DO NOT DISPOSE, just close. 
                if (_dbContext.Database.Connection.State == System.Data.ConnectionState.Open)
                    _dbContext.Database.Connection.Close();

                //_dbContext.Dispose(); //Close the connection! Bug, do not dispose if this is used with serveral services. EF is a weird singleton.
            }
            */
        }

        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();
            /*
            _transactionCount--;
            if (_transactionCount == 0)
            {
                _transaction.Rollback();
                _transaction = null;
                if (_dbContext.Database.Connection.State == System.Data.ConnectionState.Open)
                    _dbContext.Database.Connection.Close();

                //NO: bug _dbContext.Dispose(); //Close the connection!
            }
            */
        }

    }
}
