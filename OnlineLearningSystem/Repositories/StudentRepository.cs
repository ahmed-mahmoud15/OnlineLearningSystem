using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{

    //should be transferd into stored procedures

    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Student> GetWithEnrollmentsAsync(int studentId)
        {
            return await context.Students.Include(e => e.Enrollments).FirstOrDefaultAsync(e => e.Id == studentId);
        }

        public async Task<Student> GetWithFollowedInstructors(int studentId)
        {
            return await context.Students.Include(e => e.Follows).ThenInclude(e => e.Instructor).FirstOrDefaultAsync(e => e.Id == studentId);
        }

        public async Task<Student> GetWithLikedCoursesAsync(int studentId)
        {
            return await context.Students.Include(e => e.Likes).ThenInclude(e => e.Course).FirstOrDefaultAsync(e => e.Id == studentId);
        }

        public async Task<Student> GetWithPaymentsAsync(int studentId)
        {
            return await context.Students.Include(e => e.Payments).FirstOrDefaultAsync(e => e.Id == studentId);
        }
    }
}
