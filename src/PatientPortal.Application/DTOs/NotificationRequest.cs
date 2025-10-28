using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class NotificationRequest
    {
        public string PatientUserId { get; set; }
        public string Message { get; set; }
        public NotificationChannel Channel { get; set; }
    public DateTimeOffset? ScheduledOn { get; set; }
        public bool RequiresAcknowledgement { get; set; }
    }
}
