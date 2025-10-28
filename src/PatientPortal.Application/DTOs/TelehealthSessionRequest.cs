namespace PatientPortal.Application.DTOs
{
    public class TelehealthSessionRequest
    {
        public string AppointmentId { get; set; }
        public string PatientUserId { get; set; }
        public string MeetingUrl { get; set; }
        public string WaitingRoomUrl { get; set; }
        public string MonitoringDeviceId { get; set; }
    }
}
