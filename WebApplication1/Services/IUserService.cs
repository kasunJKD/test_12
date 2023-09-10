using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        User GetUsertById(string id);
        List<User> GetAll();
        List<User> GetAllFilter(int projectId, int customerId);
    }
}
