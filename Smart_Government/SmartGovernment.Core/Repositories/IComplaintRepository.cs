using SmartGovernment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Repositories
{
    public interface IComplaintRepository
    {
        void CreateAsync(Complaint complaint);
        void Update(Complaint complaint);
        void Delete(Complaint complaint);

        //Task<Complaint> GetComplaintsForUserAsync(string UserId);

        IEnumerable<Complaint> GetComplaintsByUserId(string UserId);
        IEnumerable<Complaint> GetComplaintsByMinistryId(int MinistryId);





    }
}
