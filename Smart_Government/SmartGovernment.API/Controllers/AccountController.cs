using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartGovernment.API.Dto;
using SmartGovernment.Core.Entities;
using SmartGovernment.Core.Repositories;
using SmartGovernment.Core.Services;
using System;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartGovernment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<appuser> _userManager;
        private readonly SignInManager<appuser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserMinistryRepository _userMinistryRepo;
    

        public AccountController(UserManager<appuser> userManager,
            SignInManager<appuser> signInManager,
            ITokenService tokenService,
            IUserMinistryRepository userMinistryRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userMinistryRepo = userMinistryRepo;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = new appuser()
            {
                SSN = registerDto.NationalID,
                UserName = registerDto.Email.Split("@")[0],
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = registerDto.Password,
                Address = registerDto.Address,
                IsAdmin=false,
                IsMinistryAdmin=false,
                

            };

            var result = await _userManager.CreateAsync(User,registerDto.Password);

            if (!result.Succeeded) return BadRequest(404);

            return Ok(new UserDto()
            {
                FullName = User.UserName,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
                Address = User.Address,
                IsMinistryAdmin = false,
                Token = await _tokenService.CreateToken(User,_userManager)


            }) ;
        }



        [HttpPost("MinistryAdminRegistration")]
        public async Task<ActionResult<UserDto>> MinistryAdminRegistration(RegisterDto registerDto)
        {
            var User = new appuser()
            {
                SSN = registerDto.NationalID,
                UserName = registerDto.Email.Split("@")[0],
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = registerDto.Password,
                Address = registerDto.Address,
                IsMinistryAdmin =true,
                IsAdmin=false,
                

            };

            var result = await _userManager.CreateAsync(User, registerDto.Password);

            if (!result.Succeeded) return BadRequest();
            var Ministry = registerDto.MinistryId;
            var user = User.Id;
            var AdminMinistry = new UserMinistry()
            {
                MinistryId = Ministry,
                UserMinistryAdminId = user,
            };
            _userMinistryRepo.CreateAsync(AdminMinistry);
            return Ok(new UserDto()
            {
                NationalID = User.SSN,
                FullName = User.UserName,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
                Address = User.Address,
                IsMinistryAdmin=true,
                MinistryId = Ministry,
                Token = await _tokenService.CreateToken(User, _userManager)


            });
        }



        [HttpPost("AdminRegistration")]
        public async Task<ActionResult<UserDto>> AdminRegistration(RegisterDto registerDto)
        {
            var User = new appuser()
            {
                SSN = registerDto.NationalID,
                UserName = registerDto.Email.Split("@")[0],
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = registerDto.Password,
                Address = registerDto.Address,
                IsMinistryAdmin = false,
                IsAdmin = true,


            };

            var result = await _userManager.CreateAsync(User, registerDto.Password);
            

            if (!result.Succeeded) return BadRequest(404);

            return Ok(new UserDto()
            {
               
                FullName = User.UserName,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
                Address = User.Address,
                IsMinistryAdmin = true,
                Token = await _tokenService.CreateToken(User, _userManager)


            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();
            {
                return Ok(new UserDto
                {
                    FullName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    NationalID=user.SSN,
                    IsMinistryAdmin=user.IsMinistryAdmin,
                    Token = await _tokenService.CreateToken(user,_userManager),
                    




                });
            }

        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);


            return Ok(new UserDto()
            {
                FullName = user.UserName,
                Email = user.Email,
                NationalID = user.SSN,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Token = await _tokenService.CreateToken(user, _userManager)


            });

        }


      


















    }
}
