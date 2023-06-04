using ConsumerAPI.Models;
using ConsumerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsumerAPI.Controllers
{
    [ApiController]
    [Route("Job.com/Consumer/")]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerOps _consumerOps;
        private readonly JobAppDbContext _context;
        public ConsumerController(IConsumerOps consumerOps, JobAppDbContext context)
        {
            this._consumerOps = consumerOps;
            _context = context;
        }

        [Authorize]
        [HttpGet("{email}/Details")]
        public Consumer GetConsumer(string email)
        {
            return _consumerOps.GetConsumerDetails(email);
        }

        [Authorize]
        [HttpGet("{email}/Jobs")]
        public IEnumerable<JobDetails> GetAllJobsPostedByConsumer(string email)
        {
            return _consumerOps.GetAllJobsByConsumer(email);
        }

        [Authorize]
        [HttpPost("Job/Create")]
        public JobDetails CreateAJob(JobDetails job)
        {
            return _consumerOps.CreateJob(job);
        }

        [Authorize]
        [HttpGet("Job/{jobId}/Resumes")]
        public IEnumerable<Resume> GetSelectedResumesForJob(int jobId)
        {
            return _consumerOps.GetAllSelectedResumes(jobId);
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
