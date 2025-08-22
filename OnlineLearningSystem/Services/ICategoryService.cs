using OnlineLearningSystem.Models;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategoriesWithCoursesAsync();
        public Task<Category> GetCategoryWithCoursesAsync(int categoryId);
        public Task CreateCategoryAsync(CreateCategoryViewModel model);

        public Task<string> DeleteCategoryAsync(int categoryId);
    }
}
