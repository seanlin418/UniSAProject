using System.Collections.Generic;
using System.Threading.Tasks;
using UniSA.Api.Repos;
using UniSA.Data;

namespace UniSA.Api.Services
{
    public interface IAuthService
    {
        Task<OpResult> AddUserRoles(ApplicationUser user, List<string> roles);
        Task<OpResult> DeleteUser(ApplicationUser user);
        Task<OpResult> RegisterUser(ApplicationUser newUser, string password, List<string> roles = null);
        Task<OpResult> RemoveUserRoles(ApplicationUser user, List<string> roles);
    }
}