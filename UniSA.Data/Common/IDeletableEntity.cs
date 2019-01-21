using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniSA.Data.Common
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
        string DeletedBy { get; set; }

    }
}