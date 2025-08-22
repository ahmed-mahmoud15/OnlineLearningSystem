using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsForStudentAsync(int studentId)
        {
            return await context.Enrollments.Include(e => e.Student).Include(e => e.Course).ThenInclude(e => e.Lessons).ToListAsync();
        }

        public async Task<Enrollment> GetEnrollmentWithStudentAndCourseAsync(int studentId, int courseId)
        {
            return await context.Enrollments.Include(e => e.Student).Include(e => e.Course).ThenInclude(e => e.Lessons).FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
