using System;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Domain.Entities
{
    public class SecureMessage : EntityBase
    {
        private SecureMessage()
        {
        }

        public SecureMessage(string subject, string body, string senderId, string recipientId, bool isSupportMessage)
        {
            Subject = subject;
            Body = body;
            SenderId = senderId;
            RecipientId = recipientId;
            IsSupportMessage = isSupportMessage;
            Status = MessageStatus.Sent;
        }

        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string SenderId { get; private set; }
        public string RecipientId { get; private set; }
        public bool IsSupportMessage { get; private set; }
        public MessageStatus Status { get; private set; }
        public DateTimeOffset? ReadOn { get; private set; }
        public bool IsEncrypted { get; private set; } = true;

        public void MarkAsRead()
        {
            Status = MessageStatus.Read;
            ReadOn = DateTimeOffset.UtcNow;
        }

        public void Archive()
        {
            Status = MessageStatus.Archived;
        }

        public void Flag()
        {
            Status = MessageStatus.Flagged;
        }
    }
}
