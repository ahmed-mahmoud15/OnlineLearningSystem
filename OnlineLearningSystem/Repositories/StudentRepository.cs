using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) { }

        public Task<Student> GetByIdentityIdAsync(string identityId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetWithEnrollmentsAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetWithFollowedInstructors(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetWithLikedCoursesAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetWithPaymentsAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetWithUserDataAsync(int studentId)
        {
            var student = await table.Include(e => e.IdentityUser).SingleOrDefaultAsync();

            return student ?? throw new KeyNotFoundException($"Student {studentId} not found.");
        }
    }
}
