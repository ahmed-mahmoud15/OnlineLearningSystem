using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Create(CreateCategoryViewModel model, string returnUrl)
        {
            try
            {
                await categoryService.CreateCategoryAsync(model);
                TempData["AlertMessage"] = $"Category {model.Name} Created successfully";
                TempData["AlertType"] = "success";
            }
            catch (ArgumentNullException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
            }
            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> Delete(int categoryId, string returnUrl)
        {
            try
            {
                string name = await categoryService.DeleteCategoryAsync(categoryId);
                TempData["AlertMessage"] = $"Category {name} deleted successfully";
                TempData["AlertType"] = "success";
            }
            catch (InvalidOperationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "danger";
            }
            return LocalRedirect(returnUrl);
        }
    }
}
