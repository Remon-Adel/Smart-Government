using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SmartGovernment.API.Dto
{
    public class RegisterDto
    {
        public long NationalID { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        public int MinistryId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        

    }
}
