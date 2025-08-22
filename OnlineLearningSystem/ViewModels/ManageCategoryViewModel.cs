namespace OnlineLearningSystem.ViewModels
{
    public class ManageCategoryViewModel
    {
        public CreateCategoryViewModel CreateCategory { get; set; }

        public IList<ShowCategoryViewModel> Categories { get; set; } = new List<ShowCategoryViewModel>();
    }
}
