using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Infrastructure.Security
{
    public class IdentityService : IIdentityService
    {
        private class UserRecord
        {
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public bool MultiFactorEnabled { get; set; }
            public bool BiometricEnabled { get; set; }
        }

        private readonly ConcurrentDictionary<string, UserRecord> _usersById = new ConcurrentDictionary<string, UserRecord>();
        private readonly ConcurrentDictionary<string, string> _userIdsByEmail = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public Task<string> CreateUserAsync(string email, string password, string role)
        {
            var userId = Guid.NewGuid().ToString("N");
            var record = new UserRecord
            {
                Email = email,
                PasswordHash = Hash(password),
                MultiFactorEnabled = false,
                BiometricEnabled = false
            };

            _usersById[userId] = record;
            _userIdsByEmail[email] = userId;
            return Task.FromResult(userId);
        }

        public Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            if (!_userIdsByEmail.TryGetValue(email, out var userId))
            {
                return Task.FromResult(false);
            }

            var record = _usersById[userId];
            var isValid = record.PasswordHash == Hash(password);
            return Task.FromResult(isValid);
        }

        public Task<bool> EnableMultiFactorAsync(string userId)
        {
            if (_usersById.TryGetValue(userId, out var record))
            {
                record.MultiFactorEnabled = true;
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> DisableMultiFactorAsync(string userId)
        {
            if (_usersById.TryGetValue(userId, out var record))
            {
                record.MultiFactorEnabled = false;
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> EnrollBiometricAsync(string userId, string biometricTemplate)
        {
            if (_usersById.TryGetValue(userId, out var record))
            {
                record.BiometricEnabled = true;
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        private static string Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
