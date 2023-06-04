using JobSearchApp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JobSearchApp_MVC.Controllers
{
    [Route("Job.com/Vendor/")]
    public class VendorController : Controller
    {
        [HttpPost("Register")]
        public async Task<IActionResult> SignUp([Bind("VendorName", "PhoneNumber", "WebsiteUrl",
                            "Email", "Password")] Vendor vendor)
        {
            HttpClient _client = new HttpClient();
            HttpContent content = new StringContent(JsonSerializer.Serialize(vendor), Encoding.UTF8, "application/json");
            _client.BaseAddress = new Uri("https://localhost:7174/");
            _client.DefaultRequestHeaders.Accept.Clear();
            var response = await _client.PostAsync("Job.com/Vendor/Register", content);
            return RedirectToAction("VendorLogin", "Home");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> VendorLogin([Bind("email", "password")] User user)
        {
            HttpClient _client = new HttpClient();
            HttpContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            _client.BaseAddress = new Uri("https://localhost:7174/");
            _client.DefaultRequestHeaders.Accept.Clear();
            var response = await _client.PostAsync("Job.com/Vendor/login", content);
            string token = response.Content.ReadAsStringAsync().Result;
            StoredData.StoredEmail = user.email;
            StoredData.StoredJWToken = token;
            if (token != "")
            {
                return RedirectToAction("Profile", "Vendor");
            }
            return RedirectToAction("VendorLogin", "Home");
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var email = StoredData.StoredEmail;
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7174/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Vendor/{email}/Details");
            var res = await response.Content.ReadAsStringAsync();
            var vendor = JsonConvert.DeserializeObject<Vendor>(res);
            return View(vendor);
        }

        [HttpGet("AllJobs")]
        public async Task<IActionResult> GetJobs()
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7174/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Vendor/Jobs/all");
            var res = await response.Content.ReadAsStringAsync();
            var joblist = JsonConvert.DeserializeObject<IEnumerable<JobDetails>>(res);
            if (joblist == null)
            {
                joblist = new List<JobDetails>();
            }
            return View(joblist);
        }

        //[HttpGet("Job/{jobId}/Resume/all")]
        [HttpGet]
        public async Task<IActionResult> GetResumes(int jobId)
        {
            var email = StoredData.StoredEmail;
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7174/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Vendor/{email}/Job/{jobId}/Resume/all");
            var res = await response.Content.ReadAsStringAsync();
            var resumelist = JsonConvert.DeserializeObject<IEnumerable<Resume>>(res);
            if (resumelist == null)
            {
                resumelist = new List<Resume>();
            }
            return View(resumelist);
        }

        [HttpGet("Job/Resume/{resumeId}/Select")]
        public async Task<IActionResult> SelectResume(int resumeId, int jobId)
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7174/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoredData.StoredJWToken);
            HttpResponseMessage response = await _client.GetAsync($"Job.com/Vendor/Job/Resume/{resumeId}/Select");
            var res = await response.Content.ReadAsStringAsync();
            return RedirectToAction("GetResumes");
        }
    }
}
