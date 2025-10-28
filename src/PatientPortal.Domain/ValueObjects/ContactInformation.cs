using System;

namespace PatientPortal.Domain.ValueObjects
{
    public sealed class ContactInformation
    {
        public ContactInformation(string email, string phoneNumber, string addressLine1, string city, string state, string postalCode)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required", nameof(email));
            }

            Email = email.Trim();
            PhoneNumber = phoneNumber?.Trim();
            AddressLine1 = addressLine1?.Trim();
            City = city?.Trim();
            State = state?.Trim();
            PostalCode = postalCode?.Trim();
        }

        public string Email { get; }
        public string PhoneNumber { get; }
        public string AddressLine1 { get; }
        public string AddressLine2 { get; private set; }
        public string City { get; }
        public string State { get; }
        public string PostalCode { get; }

        public ContactInformation WithAddressLine2(string addressLine2)
        {
            var copy = new ContactInformation(Email, PhoneNumber, AddressLine1, City, State, PostalCode)
            {
                AddressLine2 = addressLine2
            };

            return copy;
        }

        public override string ToString()
        {
            return $"{Email} | {PhoneNumber}";
        }
    }
}
