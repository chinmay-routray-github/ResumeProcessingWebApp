
using System.ComponentModel.DataAnnotations;

namespace JobSearchApp_MVC.Models
{
    public class ResumeTableData
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        public string? VendorEmail { get; set; }
        [Required]
        public string? filePath { get; set; }
        [Required]
        public IFormFile file { get; set; }

        public DateTime? UploadDate { get; set; }
    }
}
