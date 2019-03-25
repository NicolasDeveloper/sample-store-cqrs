using System;
namespace SampleStoreCQRS.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        int SaveChanges();
    }
}
