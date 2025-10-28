using System;

namespace PatientPortal.Application.DTOs
{
    public class PaymentRequest
    {
        public Guid BillingStatementId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethodToken { get; set; }
        public string PayerId { get; set; }
    }
}
