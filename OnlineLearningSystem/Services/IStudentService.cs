using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Services
{
    public interface IStudentService
    {
        public Task AddStudentAsync(Student student, IFormFile file);

        public Task LikeCourse(int studentId, int courseId);

        public Task FollowInstructor(int studentId, int instructorId);

        public Task EnrollInCourse(int studentId, int courseId);

        public Task MakePayment(int studentId);
    }
}
