using System.Threading.Tasks;

namespace PatientPortal.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
