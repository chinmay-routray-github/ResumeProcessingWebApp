﻿using ConsumerAPI.Models;
using ConsumerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.Controllers
{
    [ApiController]
    [Route("Job.com/Consumer/")]
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
        public Consumer Register(Consumer consumer)
        {
            return _authenticate.Register(consumer);
        }
    }
}
