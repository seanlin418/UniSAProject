using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniSA.Data.Infrastructure
{
    public interface IApplicationDBContext
    {
        IDbSet<ApplicationUser> Users { get; set; }
        void SaveChanges();
    }
}