using System.Collections.Generic;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class IntakeForm : EntityBase
    {
        private readonly List<IntakeFormField> _fields = new List<IntakeFormField>();

        private IntakeForm()
        {
        }

        public IntakeForm(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool RequiresSignature { get; private set; }

        public IReadOnlyCollection<IntakeFormField> Fields => _fields.AsReadOnly();

        public void AddField(string label, string controlType, bool required)
        {
            _fields.Add(new IntakeFormField(label, controlType, required));
        }

        public void MarkRequiresSignature()
        {
            RequiresSignature = true;
        }
    }
}
