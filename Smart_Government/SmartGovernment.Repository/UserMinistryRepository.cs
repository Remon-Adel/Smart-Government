using SmartGovernment.Core.Entities;
using SmartGovernment.Core.Repositories;
using SmartGovernment.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Repository
{
    public class UserMinistryRepository : IUserMinistryRepository
    {
        private readonly SmartContext _context;
        
     

        public UserMinistryRepository(SmartContext context)
        {
            _context = context;
          
           
        }
        public void CreateAsync(UserMinistry Userministry)
        {
            _context.Set<UserMinistry>().AddAsync(Userministry);
            _context.SaveChanges(); 
        }

        public void Delete(UserMinistry Userministry)
        {
            _context.Set<UserMinistry>().Remove(Userministry);
            _context.SaveChanges();
        }

        public IEnumerable<Ministry> GetAll()
        {
            return _context.Ministries.ToList();
        }

        

        //public IEnumerable<Complaint> GetAllComplaintsForMinistryAdmin(int MinistryId)
        //{
        //    return _context.Complaints.Where(c => c.MinistryId == MinistryId).ToList();
        //}

        public IEnumerable<Complaint> GetAllComplaintsForMinistryAdmin(int MinistryId)
        {
           return _context.Complaints.Where(c => c.MinistryId == MinistryId).ToList();
        }

        public UserMinistry GetMinistryIdOfAdminId(string AdminId)
        {
            return _context.Set<UserMinistry>().Where(c => c.UserMinistryAdminId == AdminId).FirstOrDefault();
        }

        public void Update(UserMinistry Userministry)
        {
            _context.Set<UserMinistry>().Update(Userministry);
            _context.SaveChanges();
        }
    }
}
