using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public interface IAuthenticate
    {
        string GetJwToken(User user);
        Candidate Register(Candidate candidate);
    }
}
