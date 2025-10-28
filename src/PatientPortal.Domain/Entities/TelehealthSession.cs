using System;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Domain.Entities
{
    public class TelehealthSession : EntityBase
    {
        private TelehealthSession()
        {
        }

        public TelehealthSession(string appointmentId, string patientUserId, string meetingUrl)
        {
            AppointmentId = appointmentId;
            PatientUserId = patientUserId;
            MeetingUrl = meetingUrl;
            Status = TelehealthStatus.Scheduled;
        }

        public string AppointmentId { get; private set; }
        public string PatientUserId { get; private set; }
        public string MeetingUrl { get; private set; }
        public string VirtualWaitingRoomUrl { get; private set; }
        public TelehealthStatus Status { get; private set; }
        public DateTimeOffset? StartedOn { get; private set; }
        public DateTimeOffset? EndedOn { get; private set; }
        public string MonitoringDeviceId { get; private set; }

        public void StartSession()
        {
            Status = TelehealthStatus.InProgress;
            StartedOn = DateTimeOffset.UtcNow;
        }

        public void EndSession()
        {
            Status = TelehealthStatus.Complete;
            EndedOn = DateTimeOffset.UtcNow;
        }

        public void FlagTechnicalIssue()
        {
            Status = TelehealthStatus.TechnicalIssue;
        }

        public void SetWaitingRoom(string url)
        {
            VirtualWaitingRoomUrl = url;
        }

        public void LinkMonitoringDevice(string deviceId)
        {
            MonitoringDeviceId = deviceId;
        }
    }
}
