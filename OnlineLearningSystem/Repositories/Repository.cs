
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.DTOs;

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
            if (record != null)
            {
                table.Remove(record);
            }
        }

        public async Task<T> GetWithConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await table.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
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

        public void DeleteObject(T record)
        {
            table.Remove(record);
        }


        public async Task<PaginateResultDTO<T>> GetAllPaginationAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            IQueryable<T> query = table;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            int totalCount = query.Count();

            var result = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginateResultDTO<T>() {
                Items = result,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

    }
}
