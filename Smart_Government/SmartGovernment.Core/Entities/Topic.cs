using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public ICollection<TopicMinistry> TopicMinistries { get; set; }
    }
}
