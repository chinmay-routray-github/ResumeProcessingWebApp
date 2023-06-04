using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumerAPI.Models
{
    public class Consumer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsumerId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ConsumerName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CompanyName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string WebsiteUrl { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }

    }
}
