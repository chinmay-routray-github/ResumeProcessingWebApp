using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public interface IAuthenticate
    {
        string GetJwToken(User user);
        Vendor Register(Vendor vendor);
    }
}
