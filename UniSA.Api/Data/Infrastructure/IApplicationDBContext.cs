using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniSA.Api.Data;
using UniSA.Api.Data.Common;

namespace UniSA.Api.Data
{
    public interface IApplicationDBContext
    {
        IDbSet<ApplicationUser> Users { get; set; }
        void SaveChanges();
    }
}