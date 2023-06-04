using ConsumerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.Services
{
    public class VendorOps : IVendorOps
    {
        private JobAppDbContext _context;
        public VendorOps(JobAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Vendor GetVendorDetails(string email)
        {
            return _context.AllVendors.Where(c => c.Email == email).FirstOrDefault();
        }

        public IEnumerable<JobDetails> GetAllJobs()
        {
            return _context.AllJobs.ToList();
        }

        public IEnumerable<Resume> GetAllResumesForAJob(int jobId, string vendor)
        {
            var res = _context.AllResumes.Where(c => c.JobId == jobId && c.VendorEmail == vendor 
                                && c.SelectionStatus == 0).ToList();
            if(res == null)
            {
                return null;
            }
            return res;
        }

        public Resume SelectResume(int resumeId)
        {
            var res = _context.AllResumes.Where(c => c.ResumeId == resumeId).FirstOrDefault();
            if(res != null)
            {
                res.SelectionStatus = 1;
            }
            _context.SaveChanges();
            return res;
        }

    }
}
