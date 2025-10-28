using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IEducationService
    {
        Task<IReadOnlyList<EducationResourceDto>> GetResourcesAsync(ResourceQuery query);
    }
}
