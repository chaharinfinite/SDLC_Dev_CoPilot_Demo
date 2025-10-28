using System;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class EducationResource : EntityBase
    {
        private EducationResource()
        {
        }

        public EducationResource(string title, string category, string contentUri)
        {
            Title = title;
            Category = category;
            ContentUri = contentUri;
        }

        public string Title { get; private set; }
        public string Category { get; private set; }
        public string ContentUri { get; private set; }
        public string Language { get; private set; } = "en";
        public bool RequiresAccessibilityMode { get; private set; }

        public void SetLanguage(string language)
        {
            Language = language;
        }

        public void FlagAccessibility()
        {
            RequiresAccessibilityMode = true;
        }
    }
}
