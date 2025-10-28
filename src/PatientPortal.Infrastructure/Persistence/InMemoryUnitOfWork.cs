using System.Threading.Tasks;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Infrastructure.Persistence
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }
    }
}
