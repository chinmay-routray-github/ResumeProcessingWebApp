using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public interface IAuthenticate
    {
        string GetJwToken(User user);
        Consumer Register(Consumer consumer);
    }
}
