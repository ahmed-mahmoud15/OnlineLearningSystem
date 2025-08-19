using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Student> GetStudentWithUserDataAsync(int studentId)
        {
            var student = await table.Include(e => e.IdentityUser).SingleOrDefaultAsync();

            return student ?? throw new KeyNotFoundException($"Student {studentId} not found.");
        }
    }
}
