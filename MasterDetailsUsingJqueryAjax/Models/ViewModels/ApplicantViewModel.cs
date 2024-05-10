using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetailsUsingJqueryAjax.Models.ViewModels
{
    public class ApplicantViewModel
    {
        public int Id { get; set; }
        [Required, DisplayName("Applicant Name")]
        public string ApplicantName { get; set; } = "";
        [Required,DisplayName("Date of Birth"), DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:yyyy/dd/MM}",ApplyFormatInEditMode =true)]
        public DateTime Dob { get; set; }
        public string Qualification { get; set; } = null!;
        public string MobileNo  { get; set; }
        [ForeignKey("Experiance")]
        public int ExperianceId { get; set; }
        public Experiance Experiance { get; set; }
        public IList<Experiance> Experiances { get; set; } = new List<Experiance>();
        public virtual IList<Applicant> Applicants { get; set; }= new List<Applicant>();
        public IFormFile ProfilePhoto { get; set; }
        public string DesignationName { get; set; }
        public List<Designation> Designations { get; set; }
        public int YearsWorked { get; set; }
        [Required, DisplayName("Active")]
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; } = "";

    }
}
