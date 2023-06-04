
namespace JobSearchApp_MVC.Models
{
    public class JobDetails
    {
        public int JobId { get; set; }

        public int ConsumerId { get; set; }

        public string? JobTitle { get; set; }

        public string JobDescription { get; set; }

        public string KeySkills { get; set; }

        public string? YearOfExperience { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
