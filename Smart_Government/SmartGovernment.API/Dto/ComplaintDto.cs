using System.ComponentModel.DataAnnotations;

namespace SmartGovernment.API.Dto
{
    public class ComplaintDto
    {
        [Required]
        public string OriginalComplaint { get; set; }

        
    }
}
