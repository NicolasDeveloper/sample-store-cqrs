

using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;

namespace SampleStoreCQRS.Infra.Data.Contexts.Common.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleStoreCQRSDataContext _context;

        public UnitOfWork(SampleStoreCQRSDataContext context)
        {
            _context = context;
        }

        public bool Commit() => _context.SaveChanges() > 0;
        
        public void Dispose() => _context.Dispose();
    }
}
