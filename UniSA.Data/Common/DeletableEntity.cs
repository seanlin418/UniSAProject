using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniSA.Data.Common
{
    public class DeletableEntity : IDeletableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}