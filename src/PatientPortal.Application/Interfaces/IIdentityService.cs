using System.Threading.Tasks;

namespace PatientPortal.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string> CreateUserAsync(string email, string password, string role);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
        Task<bool> EnableMultiFactorAsync(string userId);
        Task<bool> DisableMultiFactorAsync(string userId);
        Task<bool> EnrollBiometricAsync(string userId, string biometricTemplate);
    }
}
