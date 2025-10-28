using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IBillingService
    {
        Task<IReadOnlyList<BillingStatementDto>> GetStatementsAsync(string patientUserId);
        Task<PaymentReceiptDto> PayStatementAsync(PaymentRequest request);
        Task<IReadOnlyList<PaymentReceiptDto>> GetPaymentHistoryAsync(string patientUserId);
    }
}
