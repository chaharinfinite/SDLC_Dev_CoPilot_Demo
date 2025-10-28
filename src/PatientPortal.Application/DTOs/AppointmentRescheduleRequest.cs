using System;

namespace PatientPortal.Application.DTOs
{
    public class AppointmentRescheduleRequest
    {
        public DateTimeOffset NewStartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string UpdatedBy { get; set; }
    }
}
