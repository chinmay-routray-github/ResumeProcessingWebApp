using ConsumerAPI.Models;
using ConsumerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.Controllers
{
    [ApiController]
    [Route("Job.com/Vendor/")]
    public class AuthenticationController : ControllerBase
    {
        public IAuthenticate _authenticate { get; set; } 
        public AuthenticationController(IAuthenticate authenticate) 
        {
            _authenticate = authenticate;
        }

        [HttpPost("login")]
        public string Login(User user)
        {
            return _authenticate.GetJwToken(user);
        }

        [HttpPost("Register")]
        public Vendor Register(Vendor vendor)
        {
            return _authenticate.Register(vendor);
        }
    }
}
