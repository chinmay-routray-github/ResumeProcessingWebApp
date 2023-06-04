using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public class ConsumerOps : IConsumerOps
    {
        private JobAppDbContext _context;
        public ConsumerOps(JobAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Consumer GetConsumerDetails(string email)
        {
            return _context.AllConsumers.Where(c => c.Email == email).FirstOrDefault();
        }

        public IEnumerable<JobDetails> GetAllJobsByConsumer(string email)
        {
            var res = _context.AllConsumers.Where(c => c.Email == email).FirstOrDefault();
            if(res == null)
            {
                return null;
            }
            var id = res.ConsumerId;
            return _context.AllJobs.Where(d => d.ConsumerId == id).ToList();

        }

        public JobDetails CreateJob(JobDetails job)
        {
            _context.AllJobs.Add(job);
            _context.SaveChanges();
            return job;
        }

        public IEnumerable<Resume> GetAllSelectedResumes(int jobId)
        {
            return _context.AllResumes.Where(c => c.JobId == jobId && c.SelectionStatus == 1).ToList();
        }
    }
}
