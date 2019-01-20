using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UniSA.Api.Models.AppClients;

namespace UniSA.Api
{
    public class UniDBContext : IdentityDbContext<IdentityUser>
    {
        public UniDBContext() : base("UniDBConnection")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}