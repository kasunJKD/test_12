using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Customer GetCustomertById(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void Delete(int id);
    }
}
