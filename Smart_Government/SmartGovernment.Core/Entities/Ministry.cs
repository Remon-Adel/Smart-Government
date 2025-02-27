using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Entities
{
    public class Ministry
    {
        public int MinistryId { get; set; }
        public string Name { get; set; }
        public ICollection<TopicMinistry> TopicMinistries { get; set; }





        //public ICollection<Complaint> Complaints { get; set; } // Navigational Property [Many]
        //public ICollection<appuser> user { get; set; } //Navigational Property[One]
        //public ICollection<UserMinistry> UserMinistry { get; set; }  //Navigational Property[One]
    }
}
