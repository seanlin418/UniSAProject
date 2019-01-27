using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace UniSA.Api.Repos
{
    public interface IRoleRepository
    {
        Task<IdentityResult> Create(string newRole);
    }
}