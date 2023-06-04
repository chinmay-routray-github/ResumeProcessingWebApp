using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public interface IConsumerOps
    {
        Consumer GetConsumerDetails(string email);
        IEnumerable<JobDetails> GetAllJobsByConsumer(string email);
        JobDetails CreateJob(JobDetails job);
        IEnumerable<Resume> GetAllSelectedResumes(int jobId);
    }
}
