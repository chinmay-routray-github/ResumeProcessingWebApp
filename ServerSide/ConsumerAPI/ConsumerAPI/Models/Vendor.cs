using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsumerAPI.Models
{
    public class Vendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? VendorName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PhoneNumber { get; set; }

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
