using OnlineLearningSystem.Models;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IStudentService
    {
        public Task EditStudentAsync(EditStudentViewModel model);
        public Task LikeCourse(int studentId, int courseId);
        public Task DislikeCourse(int studentId, int courseId);

        public Task FollowInstructor(int studentId, int instructorId);
        public Task UnfollowInstructor(int studentId, int instructorId);

        public Task EnrollInCourse(int studentId, int courseId);

        public Task MakePayment(MakePaymentViewModel model);
    }
}
