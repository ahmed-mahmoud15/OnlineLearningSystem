using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<IEnumerable<Category>> GetAllCategoryWithCourse();
        public Task<Category> GetCategoryWithCourse(int categoryId);
    }
}
