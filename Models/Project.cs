using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Web.Models
{
    public class Project
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [MaxLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }


        public string? CreatedByUserId { get; set; }
    }
}
