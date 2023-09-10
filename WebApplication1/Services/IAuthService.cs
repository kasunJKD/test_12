using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(string userName, string password);
        Task<bool> Login(User uu);
        Task<string> GenerateTokenStringAsync(User uu);
    }
}