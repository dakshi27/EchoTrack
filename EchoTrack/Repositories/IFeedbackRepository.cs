namespace EchoTrack.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);

        void Update(T entity);   // ✔ Phase-2
        void Delete(T entity);   // ✔ Phase-2
    }
}
