using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string PatientUserId { get; set; }
        public string ProviderId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Location { get; set; }
        public AppointmentStatus Status { get; set; }
        public string ReasonForVisit { get; set; }
        public string Notes { get; set; }
    }
}
