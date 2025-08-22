using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoryWithCourse()
        {
            return await context.Categories.Include(e => e.Courses).ToListAsync();
        }

        public async Task<Category> GetCategoryWithCourse(int categoryId)
        {
            var result = await context.Categories.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == categoryId);
            return result ?? throw new InvalidOperationException();
        }
    }
}
