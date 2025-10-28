using System;

namespace PatientPortal.Application.DTOs
{
    public class BillingStatementDto
    {
        public Guid Id { get; set; }
        public string StatementNumber { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid => AmountPaid >= AmountDue;
        public string InsuranceBreakdown { get; set; }
    }
}
