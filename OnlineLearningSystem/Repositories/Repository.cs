
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;

namespace OnlineLearningSystem.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;
        protected readonly DbSet<T> table;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public async Task DeleteAsync(object id)
        {
            T record = await GetByIdAsync(id);
            table.Remove(record);
        }

        public async Task<T> GetWithConditionAsync(Expression<Func<T, bool>> predicate)
        {
            var result =  await table.FirstOrDefaultAsync(predicate);
            return result ?? throw new InvalidOperationException($"error while processing \n{predicate.ToString()}");
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var result =  await table.FindAsync(id);
            if (result == null)
            {
                throw new Exception($"There is no record of {typeof(T)} with id = {id}");
            }
            return result;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }
    }
}
