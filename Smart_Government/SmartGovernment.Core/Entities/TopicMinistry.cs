using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Entities
{
    public class TopicMinistry
    {
        public int TopicMinistryId { get; set; }
        [ForeignKey("Ministry")]
        public int MinistryId { get; set; }
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
       
    }
}
