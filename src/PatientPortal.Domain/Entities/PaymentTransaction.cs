using System;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class PaymentTransaction : EntityBase
    {
        private PaymentTransaction()
        {
        }

        public PaymentTransaction(string billingStatementId, decimal amount, string gatewayReference)
        {
            BillingStatementId = billingStatementId;
            Amount = amount;
            GatewayReference = gatewayReference;
            ProcessedOn = DateTimeOffset.UtcNow;
        }

        public string BillingStatementId { get; private set; }
        public decimal Amount { get; private set; }
        public string GatewayReference { get; private set; }
        public string Status { get; private set; } = "Pending";
        public DateTimeOffset ProcessedOn { get; private set; }
        public string FailureReason { get; private set; }

        public void MarkSuccessful()
        {
            Status = "Success";
        }

        public void MarkFailed(string reason)
        {
            Status = "Failed";
            FailureReason = reason;
        }
    }
}
