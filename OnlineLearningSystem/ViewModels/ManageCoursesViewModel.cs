namespace OnlineLearningSystem.ViewModels
{
    public class ManageCoursesViewModel
    {
        public IList<ShowCoursesInAdminViewModel> Courses { get; set; } = new List<ShowCoursesInAdminViewModel>();
    }
}
