namespace PatientPortal.Domain.Entities
{
    public class IntakeFormField
    {
        public IntakeFormField(string label, string controlType, bool required)
        {
            Label = label;
            ControlType = controlType;
            Required = required;
        }

        public string Label { get; }
        public string ControlType { get; }
        public bool Required { get; }
    }
}
