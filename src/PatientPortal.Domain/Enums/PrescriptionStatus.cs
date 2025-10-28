namespace PatientPortal.Domain.Enums
{
    public enum PrescriptionStatus
    {
        Active = 0,
        PendingApproval = 1,
        Fulfilled = 2,
        RefillRequested = 3,
        Expired = 4,
        Rejected = 5
    }
}
