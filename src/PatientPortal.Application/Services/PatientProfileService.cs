using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;
using PatientPortal.Domain.ValueObjects;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.Services
{
    public class PatientProfileService : IPatientProfileService
    {
        private readonly IRepository<PatientProfile> _profileRepository;
        private readonly IRepository<DeviceIntegration> _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientProfileService(
            IRepository<PatientProfile> profileRepository,
            IRepository<DeviceIntegration> deviceRepository,
            IUnitOfWork unitOfWork)
        {
            _profileRepository = profileRepository;
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientProfileDto> GetProfileAsync(string userId)
        {
            var profiles = await _profileRepository.SearchAsync(profile => profile.UserId == userId);
            var profile = profiles.SingleOrDefault();
            if (profile == null)
            {
                return null;
            }

            var devices = await _deviceRepository.SearchAsync(device => device.PatientUserId == userId);
            return MapProfile(profile, devices);
        }

        public async Task UpdateProfileAsync(PatientProfileUpdateRequest request)
        {
            var profiles = await _profileRepository.SearchAsync(profile => profile.UserId == request.UserId);
            var profile = profiles.SingleOrDefault();
            if (profile == null)
            {
                throw new InvalidOperationException("Patient profile not found");
            }

            profile.UpdateDemographics(request.FirstName, request.LastName, request.MiddleName, request.DateOfBirth, request.Gender);

            var contact = new ContactInformation(
                request.Email,
                request.PhoneNumber,
                request.AddressLine1,
                request.City,
                request.State,
                request.PostalCode);

            if (!string.IsNullOrWhiteSpace(request.AddressLine2))
            {
                contact = contact.WithAddressLine2(request.AddressLine2);
            }

            profile.UpdateContact(contact);

            if (request.Insurance != null)
            {
                var insurance = new InsuranceInformation(
                    request.Insurance.ProviderName,
                    request.Insurance.PolicyNumber,
                    request.Insurance.EffectiveDate,
                    request.Insurance.ExpirationDate);

                if (!string.IsNullOrWhiteSpace(request.Insurance.GroupNumber) || !string.IsNullOrWhiteSpace(request.Insurance.PlanType))
                {
                    insurance = insurance.WithGroup(request.Insurance.GroupNumber, request.Insurance.PlanType);
                }

                profile.UpdateInsurance(insurance);
            }

            profile.SetMultiFactor(request.EnableMultiFactor);
            profile.SetBiometricEnrollment(request.EnableBiometrics);
            profile.SetLanguage(request.PreferredLanguage);

            if (request.CommunicationPreferences != null)
            {
                foreach (var preference in request.CommunicationPreferences)
                {
                    if (preference != null)
                    {
                        profile.UpdateCommunicationPreference(preference.Channel, preference.Enabled);
                    }
                }
            }

            await _profileRepository.UpdateAsync(profile);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EnableAccessibilityAsync(string userId, bool enabled)
        {
            var profiles = await _profileRepository.SearchAsync(profile => profile.UserId == userId);
            var profile = profiles.SingleOrDefault();
            if (profile == null)
            {
                throw new InvalidOperationException("Patient profile not found");
            }

            profile.EnableAccessibilityMode(enabled);
            await _profileRepository.UpdateAsync(profile);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SetCommunicationPreferenceAsync(string userId, CommunicationPreferenceDto preference)
        {
            if (preference == null)
            {
                throw new ArgumentNullException(nameof(preference));
            }

            var profiles = await _profileRepository.SearchAsync(profile => profile.UserId == userId);
            var profile = profiles.SingleOrDefault();
            if (profile == null)
            {
                throw new InvalidOperationException("Patient profile not found");
            }

            profile.UpdateCommunicationPreference(preference.Channel, preference.Enabled);
            await _profileRepository.UpdateAsync(profile);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RegisterDeviceAsync(string userId, DeviceIntegrationDto device)
        {
            var profiles = await _profileRepository.SearchAsync(profile => profile.UserId == userId);
            var profile = profiles.SingleOrDefault();
            if (profile == null)
            {
                throw new InvalidOperationException("Patient profile not found");
            }

            var deviceEntity = new DeviceIntegration(device.DeviceId, device.DeviceType, device.ProviderName, userId);
            profile.RegisterDevice(deviceEntity);

            await _deviceRepository.AddAsync(deviceEntity);
            await _profileRepository.UpdateAsync(profile);
            await _unitOfWork.SaveChangesAsync();
        }

        private static PatientProfileDto MapProfile(PatientProfile profile, IEnumerable<DeviceIntegration> devices)
        {
            var dto = new PatientProfileDto
            {
                UserId = profile.UserId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.ContactInformation?.Email,
                DateOfBirth = profile.DateOfBirth,
                PreferredLanguage = profile.PreferredLanguage,
                AccessibilityModeEnabled = profile.AccessibilityModeEnabled,
                MultiFactorEnabled = profile.MultiFactorEnabled,
                BiometricEnrollmentComplete = profile.BiometricEnrollmentComplete,
                CommunicationPreferences = profile.CommunicationPreferences
                    .Select(pref => new CommunicationPreferenceDto
                    {
                        PatientUserId = profile.UserId,
                        Channel = pref.Channel,
                        Enabled = pref.Enabled
                    })
                    .ToList(),
                Devices = devices
                    .Select(device => new DeviceIntegrationDto
                    {
                        DeviceId = device.DeviceId,
                        DeviceType = device.DeviceType,
                        ProviderName = device.ProviderName,
                        PatientUserId = device.PatientUserId,
                        LinkedOn = device.LinkedOn,
                        LastSync = device.LastSync
                    })
                    .ToList(),
                Insurance = profile.InsuranceInformation == null
                    ? null
                    : new InsuranceDto
                    {
                        ProviderName = profile.InsuranceInformation.ProviderName,
                        PolicyNumber = profile.InsuranceInformation.PolicyNumber,
                        GroupNumber = profile.InsuranceInformation.GroupNumber,
                        PlanType = profile.InsuranceInformation.PlanType,
                        EffectiveDate = profile.InsuranceInformation.EffectiveDate,
                        ExpirationDate = profile.InsuranceInformation.ExpirationDate
                    }
            };

            return dto;
        }
    }
}
