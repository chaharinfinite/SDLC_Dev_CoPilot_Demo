using PatientPortal.Domain.Enums;

namespace PatientPortal.Domain.ValueObjects
{
    public sealed class CommunicationPreference
    {
        public CommunicationPreference(NotificationChannel channel, bool enabled)
        {
            Channel = channel;
            Enabled = enabled;
        }

        public NotificationChannel Channel { get; }
        public bool Enabled { get; }
    }
}
