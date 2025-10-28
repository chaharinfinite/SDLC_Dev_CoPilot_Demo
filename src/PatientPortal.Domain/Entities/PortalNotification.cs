using System;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Domain.Entities
{
    public class PortalNotification : EntityBase
    {
        private PortalNotification()
        {
        }

        public PortalNotification(string patientUserId, string message, NotificationChannel channel, DateTimeOffset? scheduledOn = null)
        {
            PatientUserId = patientUserId;
            Message = message;
            Channel = channel;
            ScheduledOn = scheduledOn ?? DateTimeOffset.UtcNow;
        }

        public string PatientUserId { get; private set; }
        public string Message { get; private set; }
        public NotificationChannel Channel { get; private set; }
        public DateTimeOffset ScheduledOn { get; private set; }
        public DateTimeOffset? DeliveredOn { get; private set; }
        public bool RequiresAcknowledgement { get; private set; }

        public void MarkDelivered()
        {
            DeliveredOn = DateTimeOffset.UtcNow;
        }

        public void RequireAcknowledgement()
        {
            RequiresAcknowledgement = true;
        }
    }
}
