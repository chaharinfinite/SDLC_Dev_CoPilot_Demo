using System;

namespace PatientPortal.Application.DTOs
{
    public class AppointmentRequest
    {
        public string PatientUserId { get; set; }
        public string ProviderId { get; set; }
        public DateTimeOffset DesiredStart { get; set; }
        public TimeSpan Duration { get; set; }
        public string Location { get; set; }
        public string ReasonForVisit { get; set; }
    }
}
