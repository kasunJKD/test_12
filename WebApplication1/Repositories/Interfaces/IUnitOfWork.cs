namespace WebApplication1.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;

        void Save();
    }
}
