using System;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Domain.Entities
{
    public class Appointment : EntityBase
    {
        private Appointment()
        {
        }

        public Appointment(string patientUserId, string providerId, DateTimeOffset startTime, TimeSpan duration, string location)
        {
            PatientUserId = patientUserId;
            ProviderId = providerId;
            StartTime = startTime;
            Duration = duration;
            Location = location;
            Status = AppointmentStatus.Pending;
        }

        public string PatientUserId { get; private set; }
        public string ProviderId { get; private set; }
        public DateTimeOffset StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string Location { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public string ReasonForVisit { get; private set; }
        public string Notes { get; private set; }

        public void Confirm()
        {
            Status = AppointmentStatus.Confirmed;
        }

        public void Reschedule(DateTimeOffset newStartTime, TimeSpan duration, string updatedBy)
        {
            StartTime = newStartTime;
            Duration = duration;
            Status = AppointmentStatus.Rescheduled;
            StampAudit(updatedBy);
        }

        public void Cancel(string updatedBy)
        {
            Status = AppointmentStatus.Cancelled;
            StampAudit(updatedBy);
        }

        public void Complete()
        {
            Status = AppointmentStatus.Completed;
        }

        public void UpdateReason(string reason)
        {
            ReasonForVisit = reason;
        }

        public void AppendNote(string note)
        {
            Notes = string.IsNullOrWhiteSpace(Notes) ? note : string.Concat(Notes, Environment.NewLine, note);
        }
    }
}
