using SampleStoreCQRS.Domain.Core.Interfaces;
using System;

namespace SampleStoreCQRS.Tests.Contexts.Common.Repositories
{
    public class RepositoryFake<TEntity> : IRepository<TEntity> where TEntity : class
    {
       
        public virtual void Update(TEntity obj)
        {
            
        }

        public void Add(TEntity obj)
        {

        }

        public virtual void Remove(Guid id)
        {
            
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void Dispose()
        {
           
        }

        
    }
}
