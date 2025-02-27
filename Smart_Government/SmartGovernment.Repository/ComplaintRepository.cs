using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartGovernment.Core.Entities;
using SmartGovernment.Core.Repositories;
using SmartGovernment.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly SmartContext _context;
       

        public ComplaintRepository(SmartContext context)
        {
            _context = context;
        }

        public void CreateAsync(Complaint complaint)
        {
             _context.Set<Complaint>().AddAsync(complaint);
            _context.SaveChanges();
        }

        public void Delete(Complaint complaint)
        {
            _context.Set<Complaint>().Remove(complaint);
            _context.SaveChanges();
        }

        public IEnumerable<Complaint> GetComplaintsByMinistryId(int MinistryId)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Complaint> GetComplaintsByMinistryId(string User)
        //=> _context.Complaints.Where(c => c. == UserId);

        public IEnumerable<Complaint> GetComplaintsByUserId(string UserId)
         => _context.Complaints.Where(c => c.UserId == UserId);
        

        //public async Task<Complaint> GetComplaintsForUserAsync(string UserId)
        //   => await _context.Set<Complaint>().FindAsync(UserId);
      





        //public async Task<IReadOnlyList<Complaint>> GetComplaintsForUserAsync(string UserId)
        //{
        //    /*var Complaint*/ /* return await _context.Set<Complaint>().Include(C => C.OriginalComplaint).ToListAsync();*/
        //    //return Complaint;

        //    //return await _context.Set<Complaint>().Where(C => C.UserId == Id).Include(C => C.OriginalComplaint).ToListAsync();

        //    //var spec = new ComplaintWithOriginalComplaintSpecification(UserId);
        //    //var complaints = await _unitOfWork.Repository<Complaint>().GetAllWithSpecAsync(spec);
        //    //return complaints;



        //}



        public void Update(Complaint complaint)
        {
            _context.Set<Complaint>().Update(complaint);
            //_context.Entry(complaint).State = EntityState.Modified;
            _context.SaveChanges();
        }

     
    }
}
