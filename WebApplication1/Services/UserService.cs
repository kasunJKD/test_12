using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<User> GetAll()
        {
            try
            {

                var va = _unitOfWork.GenericRepository<User>().GetAll(includeProperties: "Project").ToList();
                return va;

            }
            catch (Exception)
            {
                throw;
            }

            
        }

        public List<User> GetAllFilter(int projectId, int customerId)
        {
            try
            {

                var va = _unitOfWork.GenericRepository<User>().GetAll(includeProperties: "Project, Project.Customer", filter:ava => ava.Project.Id == projectId && ava.Project.Customer.Id == customerId).ToList();
                return va;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUsertById(string id)
        {
            var model = _unitOfWork.GenericRepository<User>().GetByKey(e => e.Id == id, includeProperties: "Project");
            return model;
        }
    }
}
