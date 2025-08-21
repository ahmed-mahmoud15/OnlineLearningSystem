using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IStudentService
    {
        public Task<StudentProfileViewModel> GetStudentProfileAsync(int id);
        public Task EditStudentAsync(EditStudentViewModel model);
        public Task<EditStudentViewModel> GetStudentEditAsync(int studentId);
    }
}
