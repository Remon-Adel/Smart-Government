using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Entities
{
    public class Complaint:BaseEntity
    {

        //public int Id { get; set; }
        
        public string OriginalComplaint { get; set; }
       
        public string  ComplaintSummary { get; set; }

        [ForeignKey("Ministry")]
        public int MinistryId { get; set; }

        [ForeignKey("appuser")]
        public string UserId { get; set; }

        //[ForeignKey("Ministry")]
        //public int MinistryId { get; set; }

        public appuser User { get; set; } // Navigational Property [ONE]
        public Ministry Ministry { get; set; } // Navigational Property [ONE]

    }
}
