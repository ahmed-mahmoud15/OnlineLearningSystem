using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IStudentService
    {
        public Task<StudentProfileViewModel> GetStudentProfileAsync(int id);
        public Task EditStudentAsync(EditStudentViewModel model);
        public Task<EditStudentViewModel> GetStudentEditAsync(int studentId);
        public Task LikeCourse(int studentId, int courseId);
        public Task DislikeCourse(int studentId, int courseId);

        public Task FollowInstructor(int studentId, int instructorId);
        public Task UnfollowInstructor(int studentId, int instructorId);

        public Task EnrollInCourse(int studentId, int courseId);

        public Task MakePayment(MakePaymentViewModel model);
    }
}
