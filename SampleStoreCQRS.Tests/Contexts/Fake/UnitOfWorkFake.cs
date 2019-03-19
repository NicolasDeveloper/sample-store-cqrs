using SampleStoreCQRS.Domain.Core.Interfaces;

namespace SampleStoreCQRS.Tests.Contexts.Fake
{
    public class UnitOfWorkFake : IUnitOfWork
    {
        public bool Commit()
        {
            return true;
        }

        public void Dispose()
        {
            
        }
    }
}
