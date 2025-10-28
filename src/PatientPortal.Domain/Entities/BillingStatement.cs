using System;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class BillingStatement : EntityBase
    {
        private BillingStatement()
        {
        }

        public BillingStatement(string patientUserId, decimal amountDue, DateTime dueDate, string statementNumber)
        {
            PatientUserId = patientUserId;
            AmountDue = amountDue;
            DueDate = dueDate;
            StatementNumber = statementNumber;
        }

        public string PatientUserId { get; private set; }
        public string StatementNumber { get; private set; }
        public decimal AmountDue { get; private set; }
        public decimal AmountPaid { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? PaidOn { get; private set; }
        public string InsuranceBreakdown { get; private set; }

        public void ApplyPayment(decimal amount)
        {
            AmountPaid += amount;
            if (AmountPaid >= AmountDue)
            {
                PaidOn = DateTime.UtcNow;
            }
        }

        public void SetInsuranceBreakdown(string breakdown)
        {
            InsuranceBreakdown = breakdown;
        }
    }
}
