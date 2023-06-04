using ConsumerAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ConsumerAPI.Services
{
    public class Authenticate : IAuthenticate
    {
        public JobAppDbContext _context { get; set; }
        private readonly IConfiguration _configuration;
        public Authenticate(JobAppDbContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetJwToken(User user)
        {
            var res = _context.AllCandidates.Where(c => c.Email == user.email && 
                        c.Password == GetHashString(user.password)).FirstOrDefault();
            if(res == null)
            {
                return null;
            }
            var securityKey = new SymmetricSecurityKey(
               Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsforToken = new List<Claim>();
            claimsforToken.Add(new Claim("sub", res.Email));
            claimsforToken.Add(new Claim("user", res.CandidateName));
            claimsforToken.Add(new Claim("id", res.CandidateId.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsforToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //User.storedUsername = token;
            return token;
        }
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        public Candidate Register (Candidate candidate)
        {
            if (_context.AllCandidates.Where(c => c.Email == candidate.Email).FirstOrDefault() != null)
            {
                return null;
            }
            candidate.Password = GetHashString(candidate.Password);
            _context.AllCandidates.Add(candidate);
            _context.SaveChanges();
            return candidate;
        }

    }
}
