using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class EducationService : IEducationService
    {
        private readonly IReadRepository<EducationResource> _resourceRepository;

        public EducationService(IReadRepository<EducationResource> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<IReadOnlyList<EducationResourceDto>> GetResourcesAsync(ResourceQuery query)
        {
            var criteria = query ?? new ResourceQuery();
            var resources = await _resourceRepository.ListAsync();
            return resources
                .Where(resource => string.IsNullOrWhiteSpace(criteria.Category) || resource.Category == criteria.Category)
                .Where(resource => string.IsNullOrWhiteSpace(criteria.Language) || resource.Language == criteria.Language)
                .Where(resource => !criteria.AccessibilityModeEnabled || resource.RequiresAccessibilityMode)
                .Select(resource => new EducationResourceDto
                {
                    Title = resource.Title,
                    Category = resource.Category,
                    ContentUri = resource.ContentUri,
                    Language = resource.Language,
                    RequiresAccessibilityMode = resource.RequiresAccessibilityMode
                })
                .ToList();
        }
    }
}
