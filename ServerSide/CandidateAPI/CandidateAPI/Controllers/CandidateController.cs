using ConsumerAPI.Models;
using ConsumerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.Controllers
{
    [ApiController]
    [Route("Job.com/Candidate/")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateOps _candidateOps;
        public CandidateController(ICandidateOps candidateOps)
        {
            this._candidateOps = candidateOps;
        }

        [Authorize]
        [HttpGet("{email}/Details")]
        public Candidate GetCandidate(string email)
        {
            return _candidateOps.GetCandidateDetails(email);
        }

        [Authorize]
        [HttpGet("Jobs")]
        public IEnumerable<JobDetails> GetJob()
        {
            return _candidateOps.GetJob();
        }

        [Authorize]
        [HttpGet("Job/{jobId}")]
        public JobDetails GetJobById(int jobId) 
        { 
            return _candidateOps.GetJobById(jobId);
        }

        [Authorize]
        [HttpPost ("Job/Apply")]
        public Resume ApplyForJob(Resume resume)
        {
            return _candidateOps.PostResume(resume);
        }
    }
}
