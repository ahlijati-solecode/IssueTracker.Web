using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Web.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Panjang max 100")]
        [Required(ErrorMessage = "Project Name harus diisi")]
        public string Name { get; set; } = string.Empty;


        [StringLength(500, ErrorMessage = "Panjang max 500")]
        public string? Description { get; set; }



        public DateTime? CreatedDate { get; set; }



        public string? CreatedByUserId { get; set; }


    }
}
