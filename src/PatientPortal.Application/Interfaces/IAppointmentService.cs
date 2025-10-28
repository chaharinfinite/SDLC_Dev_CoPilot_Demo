using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IReadOnlyList<AppointmentDto>> GetUpcomingAppointmentsAsync(string patientUserId);
        Task<AppointmentDto> ScheduleAppointmentAsync(AppointmentRequest request);
        Task RescheduleAppointmentAsync(Guid appointmentId, AppointmentRescheduleRequest request);
        Task CancelAppointmentAsync(Guid appointmentId, string cancelledBy);
        Task ConfirmAppointmentAsync(Guid appointmentId);
    }
}
