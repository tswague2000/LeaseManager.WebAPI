namespace LeaseManager.Core.FrameWork.Interface
{

    /// <summary>
    /// Interface générique pour les opérations CRUD de base.
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
