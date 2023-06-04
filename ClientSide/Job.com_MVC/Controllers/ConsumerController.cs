using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using JobSearchApp_MVC.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JobSearchApp_MVC.Controllers
{
    [Route("Job.com/Consumer/")]
    public class ConsumerController : Controller
    {
        /*public IActionResult signup() 
        { 
            return RedirectToAction("Register");
        }*/

        [HttpPost("Register")]
        public async Task<IActionResult> signup([Bind("ConsumerName", "PhoneNumber", "CompanyName", "WebsiteUrl",
                            "Email", "Password")] Consumer consumer)
        {
            HttpClient _client = new HttpClient();
            HttpContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(consumer), Encoding.UTF8, "application/json");
            _client.BaseAddress = new Uri("https://localhost:7129/");
            _client.DefaultRequestHeaders.Accept.Clear();
            var response = await _client.PostAsync("Job.com/Consumer/Register", content);
            return RedirectToAction("ConsumerLogin", "Home");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> ConsumerLogin([Bind("email", "password")] User user)
        {
            HttpClient _client = new HttpClient();
            HttpContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            _client.BaseAddress = new Uri("https://localhost:7129/");
            _client.DefaultRequestHeaders.Accept.Clear();
            var response = await _client.PostAsync("Job.com/Consumer/login", content);
            string token = response.Content.ReadAsStringAsync().Result;
            StoredData.StoredEmail = user.email;
            StoredData.StoredJWToken = token;
            if (token != "")
            {
                return RedirectToAction("Profile", "Consumer");
            }
            return RedirectToAction("ConsumerLogin", "Home");
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var email = StoredData.StoredEmail;
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7129/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Consumer/{email}/Details");
            var res = await response.Content.ReadAsStringAsync();
            var consumer = JsonConvert.DeserializeObject<Consumer>(res);
            StoredData.StoredId = consumer.ConsumerId;
            return View(consumer);
        }

        [HttpGet("JobsCreated")]
        public async Task<IActionResult> JobsCreated()
        {
            var email = StoredData.StoredEmail;
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7129/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Consumer/{email}/Jobs");
            var res = await response.Content.ReadAsStringAsync();
            var joblist = JsonConvert.DeserializeObject<IEnumerable<JobDetails>>(res);
            if (joblist == null)
            {
                joblist = new List<JobDetails>();
            }
            return View(joblist);
        }

        [HttpGet("MatchedResume")]
        public async Task<IActionResult> MatchedResume(int jobId)
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7129/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Consumer/Job/{jobId}/Resumes");
            var res = await response.Content.ReadAsStringAsync();
            var resumelist = JsonConvert.DeserializeObject<IEnumerable<Resume>>(res);
            if(resumelist == null)
            {
                resumelist = new List<Resume>();
            }
            return View(resumelist);
        }

        public IActionResult JobPosting()
        {
            ViewBag.message = "Creating A Job !";
            return View();
        }

        [HttpPost("CreateJob")]
        public IActionResult CreateJob([Bind("JobTitle", "JobDescription", "KeySkills", "YearOfExperience")] JobDetails job)
        {
            HttpClient _client = new HttpClient();
            job.ConsumerId = StoredData.StoredId;
            job.CreatedOn = DateTime.UtcNow.Date.Date;
            HttpContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");
            _client.BaseAddress = new Uri("https://localhost:7129/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            var response = _client.PostAsync("Job.com/Consumer/Job/Create", content).Result;
            return RedirectToAction("JobsCreated");
        }
    }
}
