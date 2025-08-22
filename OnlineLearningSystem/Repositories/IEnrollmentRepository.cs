using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        public Task<Enrollment> GetEnrollmentWithStudentAndCourseAsync(int studentId, int courseId);
        public Task<IEnumerable<Enrollment>> GetAllEnrollmentsForStudentAsync(int studentId);

    }
}
