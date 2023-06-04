
namespace JobSearchApp_MVC.Models
{
    public class Resume
    {
        public int ResumeId { get; set; }

        public int JobId { get; set; }

        public byte[] FileData { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string VendorEmail { get; set; }

        public float MatchingScore { get; set; }

        public int SelectionStatus { get; set; }

        public DateTime AppliedOn { get; set; }
    }
}
