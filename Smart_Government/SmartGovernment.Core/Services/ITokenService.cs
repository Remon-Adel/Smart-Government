using Microsoft.AspNetCore.Identity;
using SmartGovernment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(appuser user, UserManager<appuser> userManager);
    }
}
