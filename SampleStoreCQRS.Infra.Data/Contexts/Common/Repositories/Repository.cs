using System;
using Microsoft.EntityFrameworkCore;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;

namespace SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SampleStoreCQRSDataContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(SampleStoreCQRSDataContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
        
        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
