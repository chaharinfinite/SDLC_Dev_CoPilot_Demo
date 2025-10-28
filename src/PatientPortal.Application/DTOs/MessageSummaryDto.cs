using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class MessageSummaryDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Preview { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public MessageStatus Status { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public DateTimeOffset? ReadOn { get; set; }
    }
}
