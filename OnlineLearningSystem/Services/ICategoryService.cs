using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
