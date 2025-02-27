using EllipticCurve.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartGovernment.API.Dto;
using SmartGovernment.Core.Entities;
using SmartGovernment.Core.Repositories;
using SmartGovernment.Core.Services;
using SmartGovernment.Repository.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IComplaintRepository _complaintRepo;
        private readonly UserManager<appuser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserMinistryRepository _userMinistryRepos;
        private readonly IConfiguration _configuration;
        private readonly SmartContext _context;
        private readonly HttpClient client = new HttpClient();


        public UserController(IComplaintRepository ComplaintRepo,
            UserManager<appuser> userManager,
            ITokenService tokenService,
            IUserMinistryRepository userMinistryRepos,
            IConfiguration configuration,
            SmartContext context
)
        {
           
            _complaintRepo = ComplaintRepo;
            _userManager = userManager;
            _tokenService = tokenService;
            _userMinistryRepos = userMinistryRepos;
            _configuration = configuration;
            _context = context;
        }
        

        [Authorize]
        [HttpPost] // Post : api/User
        public async Task<ActionResult<ComplaintDto>> CreateComplaint(ComplaintDto complaintDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var url = "https://etahamad-jonaskoenig-topic-classification-04.hf.space/run/predict";

            var values = new Dictionary<string, object>
            {
                { "data",new string[] {complaintDto.OriginalComplaint}},
            };

            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject(responseString);
            JObject responseObj = JObject.Parse(responseString);
            var data = responseObj["data"][0]["label"];
            string topic = data.ToString();

            Console.WriteLine("this is the topic :" + topic);
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------");


            var urlsumm = "https://turtle14-text-summarization.hf.space/run/predict";
            var valuesSumm = new Dictionary<string, object>
            {
                { "data",new string[] {complaintDto.OriginalComplaint}},
            };
            var jsonSumm = JsonConvert.SerializeObject(valuesSumm);
            var contentSumm = new StringContent(jsonSumm, Encoding.UTF8, "application/json");
            var responseSumm = await client.PostAsync(urlsumm, contentSumm);
            var responseStringSumm = await responseSumm.Content.ReadAsStringAsync();
            var responseObjectSumm = JsonConvert.DeserializeObject(responseStringSumm);
            JObject responseObjSumm = JObject.Parse(responseStringSumm);
            var dataSumm = responseObjSumm["data"][0];
            string SummerizationText =dataSumm.ToString();

            Console.WriteLine("the Summerization Text will :"+SummerizationText);





            var ministryId = (from t in _context.Topics
                                  join tm in _context.TopicMinistries on t.TopicId equals tm.TopicId
                                  join m in _context.Ministries on tm.MinistryId equals m.MinistryId
                                  where t.TopicName == topic
                                  select m.MinistryId).FirstOrDefault();
                Console.WriteLine(ministryId);

            
            





            var complaint = new Complaint()
            {
                OriginalComplaint = complaintDto.OriginalComplaint,
                ComplaintSummary = SummerizationText,
                MinistryId = ministryId,
                UserId = user.Id,
            };
             _complaintRepo.CreateAsync(complaint);

            return Ok(complaint);
        }

        //[Authorize]
        //[HttpGet]   // Get :  api/User
        //public async Task<ActionResult<IReadOnlyList<Complaint>>> GetComplaintForUser()
        //{
        //    var email = User.FindFirstValue(ClaimTypes.Email);
        //    var user = await _userManager.FindByEmailAsync(email);
        //    //var user = await _userManager.FindByEmailAsync(email);
        //    var Complaint = await _complaintRepo.GetComplaintsForUserAsync(user.Id);

        //    return Ok(Complaint);
        //}

        [HttpGet]
        public IEnumerable<Ministry> GetMinistriesAsync()
        {
            var ministry =_userMinistryRepos.GetAll();
            return  (ministry);
        }

       [Authorize]
       [HttpGet("GetComplaintsAsync")]
        public async Task<IEnumerable<Complaint>> GetComplaintsAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var complaints = _complaintRepo.GetComplaintsByUserId(user.Id);
            return (complaints);
        }

        [Authorize]
        [HttpGet("GetComplaintsForMinistryAdmin")]
        public async Task<IEnumerable<Complaint>> GetComplaintsForMinistryAdmin()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
           

            if (user.IsMinistryAdmin)
            {
                var admin = _userMinistryRepos.GetMinistryIdOfAdminId(user.Id);
                var complaints = _userMinistryRepos.GetAllComplaintsForMinistryAdmin(admin.MinistryId);
                return (complaints);
            }

            return null;

            
            
        }






        
       





    }
}
