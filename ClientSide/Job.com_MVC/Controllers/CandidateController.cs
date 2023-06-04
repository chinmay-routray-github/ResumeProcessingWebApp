using JobSearchApp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JobSearchApp_MVC.Controllers
{
    [Route("Job.com/Candidate/")]
    public class CandidateController : Controller
    {
        [HttpPost("Register")]
        public async Task<IActionResult> SignUp([Bind("CandidateName", "PhoneNumber",
                            "Email", "Password")] Candidate candidate)
        {
            HttpClient _client = new HttpClient();
            HttpContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(candidate), Encoding.UTF8, "application/json");
            _client.BaseAddress = new Uri("https://localhost:7102/");
            _client.DefaultRequestHeaders.Accept.Clear();
            var response = await _client.PostAsync("Job.com/Candidate/Register", content);
            return RedirectToAction("CandidateLogin", "Home");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> CandidateLogin([Bind("email", "password")] User user)
        {
            HttpClient _client = new HttpClient();
            HttpContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            _client.BaseAddress = new Uri("https://localhost:7102/");
            _client.DefaultRequestHeaders.Accept.Clear();
            var response = await _client.PostAsync("Job.com/Candidate/login", content);
            string token = response.Content.ReadAsStringAsync().Result;
            StoredData.StoredEmail = user.email;
            StoredData.StoredJWToken = token;
            if (token != "")
            {
                return RedirectToAction("Profile", "Candidate");
            }
            return RedirectToAction("CandidateLogin", "Home");
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var email = StoredData.StoredEmail;
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7102/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Candidate/{email}/Details");
            var res = await response.Content.ReadAsStringAsync();
            var candidate = JsonConvert.DeserializeObject<Candidate>(res);
            return View(candidate);
        }

        [HttpGet("AllJobs")]
        public async Task<IActionResult> GetJobs()
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7102/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Candidate/Jobs");
            var res = await response.Content.ReadAsStringAsync();
            var joblist = JsonConvert.DeserializeObject<IEnumerable<JobDetails>>(res);
            if (joblist == null)
            {
                joblist = new List<JobDetails>();
            }
            return View(joblist);
        }

        public IActionResult ApplicationForm(int jobId)
        {
            ViewBag.JobId = jobId;
            return View();
        }

        [HttpPost("Job/Resume/Upload")]
        public async Task<IActionResult> ApplyForJob([Bind("JobId", "VendorEmail", "filePath", "file")] ResumeTableData MyFile)
        {
            Resume resume = new Resume();
            using(var stream = new MemoryStream())
            {
                resume.AppliedOn = DateTime.Now.Date;
                resume.VendorEmail = MyFile.VendorEmail;
                resume.SelectionStatus = 0;
                resume.FileName = MyFile.file.FileName;
                resume.ContentType = MyFile.file.ContentType;
                resume.JobId = MyFile.JobId;
                MyFile.file.CopyTo(stream);
                resume.FileData = stream.ToArray();
                MyFile.UploadDate = DateTime.Now;
            }
            
            HttpClient _client = new HttpClient();

            //Resume Scoring Algorithm
            _client.BaseAddress = new Uri("https://localhost:7102/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Candidate/Job/{MyFile.JobId}");
            var res = await response.Content.ReadAsStringAsync();
            var job = JsonConvert.DeserializeObject<JobDetails>(res);
            string[] KeySkills = job.KeySkills.Split(',');
            resume.MatchingScore = PdfScorer.score(KeySkills, PdfScorer.GetText(MyFile.filePath));

            //Uploading the Resume
            HttpContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(resume), Encoding.UTF8, "application/json");
            /*_client.BaseAddress = new Uri("https://localhost:7102/");
            _client.DefaultRequestHeaders.Accept.Clear();*/
            var ponse = await _client.PostAsync("Job.com/Candidate/Job/Apply", content);
            return RedirectToAction("GetJobs");
        }
    }
}
