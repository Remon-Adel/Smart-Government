using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SmartGovernment.Core.Entities;
using SmartGovernment.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.Service
{
    public class TokenService : ITokenService
    {
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<string> CreateToken(appuser user, UserManager<appuser> userManager)
        {
            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.UserName)
            };
            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRole)
                authclaims.Add(new Claim(ClaimTypes.Role, role));

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["jwt:ValidIssuer"],
                audience: Configuration["jwt:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(Configuration["jwt:DurationInDays"])),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);



        }
    }
}
