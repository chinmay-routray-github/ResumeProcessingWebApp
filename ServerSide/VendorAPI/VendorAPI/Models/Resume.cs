using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsumerAPI.Models
{
    public class Resume
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResumeId { get; set; }

        [Required]
        [ForeignKey("JobId")]
        //public JobDetails? JobDetails { get; set; }
        public int JobId { get; set; }

        public byte[] FileData { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FileName { get; set; }

        public string ContentType { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        //public Vendor? Vendor { get; set; }
        public string VendorEmail { get; set; }

        [Required]
        public int MatchingScore { get; set; }

        public int SelectionStatus { get; set; }

        public DateTime AppliedOn { get; set; }
    }
}
