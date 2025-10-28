namespace PatientPortal.Domain.Entities
{
    public class LabResultMetric
    {
        public LabResultMetric(string name, string value, string unit, string referenceRange)
        {
            Name = name;
            Value = value;
            Unit = unit;
            ReferenceRange = referenceRange;
        }

        public string Name { get; }
        public string Value { get; }
        public string Unit { get; }
        public string ReferenceRange { get; }
    }
}
