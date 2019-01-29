using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace UniSA.Api.Repos
{
    public interface IRoleRepository
    {
        Task<OpResult> AddRoles(string userId, IEnumerable<string> roles, OpResult results = null);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IEnumerable<string>> GetRoles(string userId, OpResult results = null);
        Task<OpResult> RemoveRoles(string userId, IEnumerable<string> roles, OpResult results = null);
    }
}