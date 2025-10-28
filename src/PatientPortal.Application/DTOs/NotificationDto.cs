using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public NotificationChannel Channel { get; set; }
        public DateTimeOffset ScheduledOn { get; set; }
        public DateTimeOffset? DeliveredOn { get; set; }
        public bool RequiresAcknowledgement { get; set; }
    }
}
