using System.Security.Principal;

namespace SmartGovernment.API.Dto
{
    public class UserDto
    {
        public long NationalID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber {get; set; }
        public bool IsMinistryAdmin { get; set; }
        public int MinistryId { get; set; }
        public string Token { get; set; }

    }
}
