using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public interface ICandidateOps
    {
        Candidate GetCandidateDetails(string email);
        IEnumerable<JobDetails> GetJob();
        JobDetails GetJobById(int jobId);
        Resume PostResume(Resume resume);
    }
}
