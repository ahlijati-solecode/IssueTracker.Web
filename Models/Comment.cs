using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Web.Models
{
    public class Comment
    {
        internal object comment;

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Models.Issue))]
        [Required(ErrorMessage = "Issue Id is required")]
        public int IssueId { get; set; }

        public Issue? Issue { get; set; }

        //[Required(ErrorMessage = "UserID is required")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(100, ErrorMessage = "Content Name cannot be longer than 100 character")]
        public string Content { get; set; }

        //[Required(ErrorMessage = "Created Date is required")]
        public DateTime? CreatedDate { get; set; }
    }
}
