using System.Linq.Expressions;

namespace OnlineLearningSystem.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(object id);
        public Task<T> GetWithConditionAsync(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> predicate);
        public Task AddAsync(T entity);
        public void Update(T entity);
        public Task DeleteAsync(object id);
        public void DeleteObject(T record);
        public Task SaveAsync();
    }
}
