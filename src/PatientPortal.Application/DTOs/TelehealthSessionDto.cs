using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class TelehealthSessionDto
    {
        public Guid Id { get; set; }
        public string AppointmentId { get; set; }
    public string PatientUserId { get; set; }
        public string MeetingUrl { get; set; }
        public string VirtualWaitingRoomUrl { get; set; }
        public TelehealthStatus Status { get; set; }
        public DateTimeOffset? StartedOn { get; set; }
        public DateTimeOffset? EndedOn { get; set; }
        public string MonitoringDeviceId { get; set; }
    }
}
