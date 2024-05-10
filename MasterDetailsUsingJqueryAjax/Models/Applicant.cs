using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetailsUsingJqueryAjax.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ApplicantName { get; set; } = "";
       
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public string DesignationName { get; set; } = "";
        public string MobileNo { get; set; } = "";
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        
        public virtual ICollection<Experiance> Experiances { get; set; } = new List<Experiance>();
    }
}
