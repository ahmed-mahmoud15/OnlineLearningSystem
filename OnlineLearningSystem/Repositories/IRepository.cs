namespace OnlineLearningSystem.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(object id);

        public Task AddAsync(T entity);
        public void Update(T entity);
        public Task DeleteAsync(object id);
        public Task SaveAsync();
    }
}
