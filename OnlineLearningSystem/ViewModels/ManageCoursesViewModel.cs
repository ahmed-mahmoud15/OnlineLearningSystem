namespace OnlineLearningSystem.ViewModels
{
    public class ManageCoursesViewModel
    {
        public IList<ShowCoursesInHomeViewModel> Courses { get; set; } = new List<ShowCoursesInHomeViewModel>();
    }
}
