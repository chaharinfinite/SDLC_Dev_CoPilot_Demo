using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using PatientPortal.Application.Interfaces;
using PatientPortal.Application.Services;
using PatientPortal.Infrastructure.Configuration;
using PatientPortal.Infrastructure.Persistence;
using PatientPortal.Web.Infrastructure;

namespace PatientPortal.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static InMemoryDataStore _dataStore;
        private static InMemoryUnitOfWork _unitOfWork;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapServices();
        }

        private static void BootstrapServices()
        {
            var persistence = InfrastructureModule.CreateInMemoryPersistence();
            _dataStore = persistence.dataStore;
            _unitOfWork = persistence.unitOfWork;

            var profileRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.PatientProfile>(_dataStore);
            var deviceRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.DeviceIntegration>(_dataStore);
            var appointmentRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.Appointment>(_dataStore);
            var messageRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.SecureMessage>(_dataStore);
            var recordRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.MedicalRecord>(_dataStore);
            var labRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.LabResult>(_dataStore);
            var prescriptionRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.Prescription>(_dataStore);
            var documentRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.DocumentSubmission>(_dataStore);
            var educationRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.EducationResource>(_dataStore);
            var notificationRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.PortalNotification>(_dataStore);
            var telehealthRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.TelehealthSession>(_dataStore);
            var billingRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.BillingStatement>(_dataStore);
            var paymentRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.PaymentTransaction>(_dataStore);
            var symptomRepository = InfrastructureModule.CreateRepository<PatientPortal.Domain.Entities.SymptomAssessment>(_dataStore);

            var notificationGateway = InfrastructureModule.CreateNotificationGateway();
            var triageClient = InfrastructureModule.CreateTriageClient();
            var uploadRoot = HostingEnvironment.MapPath("~/App_Data/Uploads") ?? "Uploads";
            var fileStorage = InfrastructureModule.CreateFileStorageService(uploadRoot);

            var notificationService = new NotificationService(notificationRepository, notificationGateway, _unitOfWork);

            var services = new Dictionary<Type, object>
            {
                { typeof(IPatientProfileService), new PatientProfileService(profileRepository, deviceRepository, _unitOfWork) },
                { typeof(IAppointmentService), new AppointmentService(appointmentRepository, notificationService, _unitOfWork) },
                { typeof(IMessagingService), new MessagingService(messageRepository, _unitOfWork) },
                { typeof(IMedicalRecordService), new MedicalRecordService(recordRepository, labRepository) },
                { typeof(IPrescriptionService), new PrescriptionService(prescriptionRepository, _unitOfWork) },
                { typeof(IDocumentService), new DocumentService(documentRepository, fileStorage, _unitOfWork) },
                { typeof(IEducationService), new EducationService(educationRepository) },
                { typeof(INotificationService), notificationService },
                { typeof(ITelehealthService), new TelehealthService(telehealthRepository, notificationService, _unitOfWork) },
                { typeof(IDeviceIntegrationService), new DeviceIntegrationService(deviceRepository, _unitOfWork) },
                { typeof(ISymptomTriageService), new SymptomTriageService(symptomRepository, triageClient, _unitOfWork) },
                { typeof(IBillingService), new BillingService(billingRepository, paymentRepository, _unitOfWork) }
            };

            DependencyResolver.SetResolver(new SimpleDependencyResolver(services));
        }
    }
}
