using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public class CandidateOps : ICandidateOps
    {
        private JobAppDbContext _context;
        public CandidateOps(JobAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Candidate GetCandidateDetails(string email)
        {
            return _context.AllCandidates.Where(c => c.Email == email).FirstOrDefault();
        }

        public IEnumerable<JobDetails> GetJob()
        {
            var res = _context.AllJobs.ToList();
            if(res == null)
            {
                return null;
            }
            return res;
        }

        public JobDetails GetJobById(int jobId)
        {
            var res = _context.AllJobs.Where(c => c.JobId == jobId).FirstOrDefault();
            if (res == null)
            {
                return null;
            }
            return res;
        }

        public Resume PostResume(Resume resume)
        {
            _context.AllResumes.Add(resume);
            _context.SaveChanges();
            return resume;
        }
    }
}
