using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        public Task<Student> GetWithEnrollmentsAsync(int studentId);

        public Task<Student> GetWithPaymentsAsync(int studentId);

        public Task<Student> GetWithLikedCoursesAsync(int studentId);

        public Task<Student> GetWithFollowedInstructors(int studentId);
    }
}
