using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;  //needed for IDbContextTransaction
using SCAMS.Repository.EntityModels;

namespace SCAMS.Repository.Context
{
    public class ScamsDBContext : DbContext,  IDisposable
    {
        private IDbContextTransaction _transaction; 
        private bool disposed = false;
        public ScamsDBContext(DbContextOptions<ScamsDBContext> options) : base(options)
        {

        }

        public ScamsDBContext() : base()
        {

        }

        public DbSet<Agency> Agency { get; set; }  
        public DbSet<Category> Category { get; set; }

        
        
    }
}
