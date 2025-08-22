namespace OnlineLearningSystem.ViewModels
{
    public class ManageInstructorViewModel
    {
        public CreateInstructorViewModel CreateInstructor { get; set; }

        public IList<ShowInstructorAdminInforViewModel> Instructors { get; set; } = new List<ShowInstructorAdminInforViewModel>();
    }
}
