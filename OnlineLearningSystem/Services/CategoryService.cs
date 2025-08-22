using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateCategoryAsync(CreateCategoryViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("category model is null");
            }
            
            Category category = new Category() {
                Name = model.Name
            };
            await unitOfWork.Categories.AddAsync(category);
            await unitOfWork.CompleteAsync();
        }

        public async Task<string> DeleteCategoryAsync(int categoryId)
        {
            Category category = await CheckEntity.CheckAndGetCategoryAsync(categoryId, unitOfWork);

            if (category.Courses.Count > 0) {
                throw new InvalidOperationException("Can't Delete Category Contains Courses");
            }
            unitOfWork.Categories.DeleteObject(category);
            await unitOfWork.CompleteAsync();
            return category.Name;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await unitOfWork.Categories.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithCoursesAsync()
        {
            return await unitOfWork.Categories.GetAllCategoryWithCourse();
        }

        public async Task<Category> GetCategoryWithCoursesAsync(int categoryId)
        {
            return await unitOfWork.Categories.GetCategoryWithCourse(categoryId);
        }
    }
}
