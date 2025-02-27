using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartGovernment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Repository.Data
{
    public class SmartContext:IdentityDbContext<appuser>
    {
        public SmartContext(DbContextOptions<SmartContext> options):base(options)
        {

        }
       
        public DbSet<Ministry> Ministries { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicMinistry> TopicMinistries { get; set; }





    }
}
