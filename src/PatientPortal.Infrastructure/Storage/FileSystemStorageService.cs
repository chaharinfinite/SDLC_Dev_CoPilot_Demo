using System;
using System.IO;
using System.Threading.Tasks;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Infrastructure.Storage
{
    public class FileSystemStorageService : IFileStorageService
    {
        private readonly string _rootDirectory;

        public FileSystemStorageService(string rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }

        public Task<string> UploadAsync(string fileName, byte[] content, string contentType)
        {
            if (!Directory.Exists(_rootDirectory))
            {
                Directory.CreateDirectory(_rootDirectory);
            }

            var safeFileName = Path.GetFileName(fileName);
            var fullPath = Path.Combine(_rootDirectory, $"{Guid.NewGuid():N}_{safeFileName}");
            File.WriteAllBytes(fullPath, content ?? Array.Empty<byte>());
            return Task.FromResult(fullPath);
        }
    }
}
