using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{

    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Student>> GetAllWithIdentityEnrollmentsAsync()
        {
            return await context.Students.Include(e => e.IdentityUser).Include(e => e.Enrollments).ToListAsync();
        }

        public async Task<int> GetTotalNumberOfStudentsAsync()
        {
            return await context.Students.CountAsync();
        }

        public async Task<Student> GetWithEnrollmentsAsync(int studentId)
        {
            var result = await context.Students.Include(e => e.Enrollments).ThenInclude(e => e.Course).FirstOrDefaultAsync(e => e.Id == studentId);
            return result ?? throw new InvalidOperationException();
        }

        public async Task<Student> GetWithFollowedInstructors(int studentId)
        {
            var result = await context.Students.Include(e => e.Follows).ThenInclude(e => e.Instructor).FirstOrDefaultAsync(e => e.Id == studentId);
            return result ?? throw new InvalidOperationException();
        }        

        public async Task<Student> GetWithLikedCoursesAsync(int studentId)
        {
            var result = await context.Students.Include(e => e.Likes).ThenInclude(e => e.Course).FirstOrDefaultAsync(e => e.Id == studentId);
            return result ?? throw new InvalidOperationException();
        }

        public async Task<Student> GetWithPaymentsAsync(int studentId)
        {
            var result = await context.Students.Include(e => e.Payments).FirstOrDefaultAsync(e => e.Id == studentId);
            return result ?? throw new InvalidOperationException();
        }
    }
}
