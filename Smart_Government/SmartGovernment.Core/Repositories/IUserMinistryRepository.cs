using SmartGovernment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Repositories
{
    public interface IUserMinistryRepository
    {
        void CreateAsync(UserMinistry Userministry);
        void Update(UserMinistry Userministry);
        void Delete(UserMinistry Userministry);

        //Task<IReadOnlyList<Ministry>> GetDataOfMinistry();
        IEnumerable<Ministry> GetAll();
        IEnumerable<Complaint> GetAllComplaintsForMinistryAdmin(int MinistryId);

        UserMinistry GetMinistryIdOfAdminId(string AdminId);

      

    }
}
