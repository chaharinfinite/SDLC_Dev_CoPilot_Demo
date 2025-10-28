using System;

namespace PatientPortal.Application.DTOs
{
    public class PaymentReceiptDto
    {
        public Guid TransactionId { get; set; }
        public string StatementNumber { get; set; }
        public decimal Amount { get; set; }
        public string GatewayReference { get; set; }
        public string Status { get; set; }
        public DateTimeOffset ProcessedOn { get; set; }
    }
}
