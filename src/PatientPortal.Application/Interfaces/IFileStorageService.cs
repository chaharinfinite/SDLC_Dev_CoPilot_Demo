using System.Threading.Tasks;

namespace PatientPortal.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(string fileName, byte[] content, string contentType);
    }
}
