using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Entities
{
    public class UserMinistry
    {
        public int Id { get; set; }

        [ForeignKey("Ministry")]
        public int MinistryId { get; set; }
        [ForeignKey("appuser")]
        public string UserMinistryAdminId { get; set; }

        public ICollection<appuser> User { get; set; } // Navigational Property [Many]
        public Ministry Ministry { get; set; } // Navigational Property [Many]


       


    }
}
