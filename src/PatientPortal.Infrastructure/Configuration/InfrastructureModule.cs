using PatientPortal.Application.Interfaces;
using PatientPortal.Infrastructure.AI;
using PatientPortal.Infrastructure.Notifications;
using PatientPortal.Infrastructure.Persistence;
using PatientPortal.Infrastructure.Security;
using PatientPortal.Infrastructure.Storage;

namespace PatientPortal.Infrastructure.Configuration
{
    public static class InfrastructureModule
    {
        public static (InMemoryDataStore dataStore, InMemoryUnitOfWork unitOfWork) CreateInMemoryPersistence()
        {
            var store = new InMemoryDataStore();
            var unitOfWork = new InMemoryUnitOfWork();
            return (store, unitOfWork);
        }

        public static IRepository<T> CreateRepository<T>(InMemoryDataStore dataStore) where T : PatientPortal.Domain.Common.EntityBase
        {
            return new InMemoryRepository<T>(dataStore);
        }

        public static IIdentityService CreateIdentityService()
        {
            return new IdentityService();
        }

        public static IFileStorageService CreateFileStorageService(string root)
        {
            return new FileSystemStorageService(root);
        }

        public static INotificationGateway CreateNotificationGateway()
        {
            return new ConsoleNotificationGateway();
        }

        public static IAiTriageClient CreateTriageClient()
        {
            return new RuleBasedAiTriageClient();
        }
    }
}
