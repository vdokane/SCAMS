using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCAMS.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public interface IUnitOfWork : IDisposable
        {
            Task<int> SaveAsync();
            Task BeginTransactionAsync();

            void Commit();
            void Rollback();

            //Dont like having this here but until I can figure out how to pass the dbContext around..
            IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        }
    }
}
