using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsumerAPI.Models
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidateId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? CandidateName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
    }
}
