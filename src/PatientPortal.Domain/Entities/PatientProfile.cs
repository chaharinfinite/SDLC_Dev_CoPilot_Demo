using System;
using System.Collections.Generic;
using System.Linq;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.Enums;
using PatientPortal.Domain.ValueObjects;

namespace PatientPortal.Domain.Entities
{
    public class PatientProfile : EntityBase
    {
        private readonly List<CommunicationPreference> _communicationPreferences = new List<CommunicationPreference>();
        private readonly List<DeviceIntegration> _devices = new List<DeviceIntegration>();

        private PatientProfile()
        {
        }

        public PatientProfile(string userId, string firstName, string lastName, ContactInformation contactInformation)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User id is required", nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name is required", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name is required", nameof(lastName));
            }

            UserId = userId;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            ContactInformation = contactInformation;
            DateOfBirth = null;
            PreferredLanguage = "en";
        }

        public string UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string Gender { get; private set; }
        public ContactInformation ContactInformation { get; private set; }
        public InsuranceInformation InsuranceInformation { get; private set; }
        public bool MultiFactorEnabled { get; private set; }
        public bool BiometricEnrollmentComplete { get; private set; }
        public string PreferredLanguage { get; private set; }
        public bool AccessibilityModeEnabled { get; private set; }

        public IReadOnlyCollection<CommunicationPreference> CommunicationPreferences => _communicationPreferences.AsReadOnly();
        public IReadOnlyCollection<DeviceIntegration> ConnectedDevices => _devices.AsReadOnly();

        public void UpdateDemographics(string firstName, string lastName, string middleName, DateTime? dateOfBirth, string gender)
        {
            FirstName = firstName?.Trim() ?? FirstName;
            LastName = lastName?.Trim() ?? LastName;
            MiddleName = middleName?.Trim();
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public void UpdateContact(ContactInformation contactInformation)
        {
            ContactInformation = contactInformation ?? throw new ArgumentNullException(nameof(contactInformation));
        }

        public void UpdateInsurance(InsuranceInformation insuranceInformation)
        {
            InsuranceInformation = insuranceInformation;
        }

        public void SetMultiFactor(bool enabled)
        {
            MultiFactorEnabled = enabled;
        }

        public void SetBiometricEnrollment(bool completed)
        {
            BiometricEnrollmentComplete = completed;
        }

        public void SetLanguage(string language)
        {
            PreferredLanguage = string.IsNullOrWhiteSpace(language) ? PreferredLanguage : language.Trim();
        }

        public void EnableAccessibilityMode(bool enabled)
        {
            AccessibilityModeEnabled = enabled;
        }

        public void UpdateCommunicationPreference(NotificationChannel channel, bool enabled)
        {
            var existing = _communicationPreferences.SingleOrDefault(pref => pref.Channel == channel);
            if (existing != null)
            {
                _communicationPreferences.Remove(existing);
            }

            _communicationPreferences.Add(new CommunicationPreference(channel, enabled));
        }

        public void RegisterDevice(DeviceIntegration device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            _devices.RemoveAll(existing => existing.DeviceId == device.DeviceId);
            _devices.Add(device);
        }
    }
}
