using ConsumerAPI.Models;
using ConsumerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsumerAPI.Controllers
{
    [ApiController]
    [Route("Job.com/Vendor/")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorOps _vendorOps;
        private readonly JobAppDbContext _context;
        public VendorController(IVendorOps vendorOps, JobAppDbContext context)
        {
            this._vendorOps = vendorOps;
            _context = context;
        }

        [Authorize]
        [HttpGet("{email}/Details")]
        public Vendor GetVendor(string email)
        {
            return _vendorOps.GetVendorDetails(email);
        }

        [Authorize]
        [HttpGet("Jobs/all")]
        public IEnumerable<JobDetails> GetAllJobsPosted()
        {
            return _vendorOps.GetAllJobs();
        }

        [Authorize]
        [HttpGet("{email}/Job/{jobId}/Resume/all")]
        public IEnumerable<Resume> GetAllResume(int jobId, string email)
        {
            return _vendorOps.GetAllResumesForAJob(jobId, email);
        }

        [Authorize]
        [HttpGet("Job/Resume/{resumeId}/Select")]
        public Resume SelectResumeForJob(int resumeId)
        {
            return _vendorOps.SelectResume(resumeId);
        }

        [HttpGet("Job/Download/{resumeId}")]
        public async Task<IActionResult> DownloadResume(int resumeId)
        {
            var pdfModel = await _context.AllResumes.FindAsync(resumeId);
            if (pdfModel == null)
            {
                return null;
            }

            var file = File(pdfModel.FileData, "application/pdf", pdfModel.FileName);

            return file;
        }
    }
}
