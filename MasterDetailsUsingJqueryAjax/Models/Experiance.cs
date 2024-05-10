using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetailsUsingJqueryAjax.Models
{
    public class Experiance
    {
        [Key]
        public int ExperianceId { get; set; }
        [ForeignKey("Applicant")]
        public int  ApplicantId { get; set; }
        public Applicant Applicant { get; private set; }
        public string CompanyName { get; set; }       
        public int YearsWorked { get; set; }
    }
}