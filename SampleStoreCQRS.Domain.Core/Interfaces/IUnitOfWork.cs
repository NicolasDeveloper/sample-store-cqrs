using System;

namespace SampleStoreCQRS.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
