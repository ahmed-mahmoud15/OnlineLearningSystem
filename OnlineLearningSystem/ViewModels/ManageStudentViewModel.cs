namespace OnlineLearningSystem.ViewModels
{
    public class ManageStudentViewModel
    {
        public IList<ShowStudentAdminInfoViewModel> Students { get; set; } = new List<ShowStudentAdminInfoViewModel>();
    }
}
