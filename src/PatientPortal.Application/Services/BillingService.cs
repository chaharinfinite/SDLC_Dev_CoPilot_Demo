using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class BillingService : IBillingService
    {
        private readonly IRepository<BillingStatement> _statementRepository;
        private readonly IRepository<PaymentTransaction> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BillingService(
            IRepository<BillingStatement> statementRepository,
            IRepository<PaymentTransaction> paymentRepository,
            IUnitOfWork unitOfWork)
        {
            _statementRepository = statementRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<BillingStatementDto>> GetStatementsAsync(string patientUserId)
        {
            var statements = await _statementRepository.SearchAsync(statement => statement.PatientUserId == patientUserId);
            return statements
                .OrderByDescending(statement => statement.DueDate)
                .Select(statement => new BillingStatementDto
                {
                    Id = statement.Id,
                    StatementNumber = statement.StatementNumber,
                    AmountDue = statement.AmountDue,
                    AmountPaid = statement.AmountPaid,
                    DueDate = statement.DueDate,
                    InsuranceBreakdown = statement.InsuranceBreakdown
                })
                .ToList();
        }

        public async Task<PaymentReceiptDto> PayStatementAsync(PaymentRequest request)
        {
            var statement = await _statementRepository.GetByIdAsync(request.BillingStatementId);
            if (statement == null)
            {
                throw new InvalidOperationException("Billing statement not found");
            }

            if (request.Amount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero", nameof(request.Amount));
            }

            statement.ApplyPayment(request.Amount);
            await _statementRepository.UpdateAsync(statement);

            var transaction = new PaymentTransaction(request.BillingStatementId.ToString(), request.Amount, request.PaymentMethodToken);
            transaction.MarkSuccessful();
            await _paymentRepository.AddAsync(transaction);

            await _unitOfWork.SaveChangesAsync();

            return new PaymentReceiptDto
            {
                TransactionId = transaction.Id,
                StatementNumber = statement.StatementNumber,
                Amount = transaction.Amount,
                GatewayReference = transaction.GatewayReference,
                Status = transaction.Status,
                ProcessedOn = transaction.ProcessedOn
            };
        }

        public async Task<IReadOnlyList<PaymentReceiptDto>> GetPaymentHistoryAsync(string patientUserId)
        {
            var statements = await _statementRepository.SearchAsync(statement => statement.PatientUserId == patientUserId);
            var statementLookup = statements.ToDictionary(statement => statement.Id.ToString());
            var payments = await _paymentRepository.ListAsync();

            return payments
                .Where(payment => statementLookup.ContainsKey(payment.BillingStatementId))
                .OrderByDescending(payment => payment.ProcessedOn)
                .Select(payment => new PaymentReceiptDto
                {
                    TransactionId = payment.Id,
                    StatementNumber = statementLookup[payment.BillingStatementId].StatementNumber,
                    Amount = payment.Amount,
                    GatewayReference = payment.GatewayReference,
                    Status = payment.Status,
                    ProcessedOn = payment.ProcessedOn
                })
                .ToList();
        }
    }
}
