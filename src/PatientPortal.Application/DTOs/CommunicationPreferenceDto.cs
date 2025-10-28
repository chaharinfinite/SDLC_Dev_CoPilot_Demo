using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class CommunicationPreferenceDto
    {
        public string PatientUserId { get; set; }
        public NotificationChannel Channel { get; set; }
        public bool Enabled { get; set; }
    }
}
