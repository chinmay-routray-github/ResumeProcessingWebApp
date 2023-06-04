using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsumerAPI.Models
{
    public class JobDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        [Required]
        [ForeignKey("ConsumerId")]
        //public Consumer? Consumer { get; set; }
        public int ConsumerId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? JobTitle { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public string JobDescription { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string KeySkills { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? YearOfExperience { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
