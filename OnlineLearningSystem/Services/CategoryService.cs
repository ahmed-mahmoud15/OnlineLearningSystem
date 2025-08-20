using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return unitOfWork.Categories.GetAllAsync();
        }
    }
}
