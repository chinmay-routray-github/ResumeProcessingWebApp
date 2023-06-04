using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public interface IVendorOps
    {
        Vendor GetVendorDetails(string email);
        IEnumerable<JobDetails> GetAllJobs();
        IEnumerable<Resume> GetAllResumesForAJob(int jobId, string vendor);
        Resume SelectResume(int resumeId);
    }
}
