using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Entities
{
    public class appuser:IdentityUser
    {
        public long SSN { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public bool IsMinistryAdmin { get; set; }

        public bool IsAdmin { get; set; }

        public UserMinistry UserMinistry { get; set; } // Navigational Property [ONE]
        //public ICollection<Complaint> Complaints { get; set; }

    }
}
