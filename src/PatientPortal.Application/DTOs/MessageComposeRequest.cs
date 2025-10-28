namespace PatientPortal.Application.DTOs
{
    public class MessageComposeRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public bool IsSupportMessage { get; set; }
    }
}
