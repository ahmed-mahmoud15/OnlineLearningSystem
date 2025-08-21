using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Instructor>> GetAllWithCoursesFollowersAsync()
        {
            var result = await context.Instructors.Include(e => e.Courses).ThenInclude(e => e.Enrollments).Include(e => e.FollowedBy).ToListAsync();
            return result ?? throw new InvalidOperationException();
        }

        public async Task<Instructor> GetWithCoursesAsync(int instructorId)
        {
            var result = await context.Instructors.Include(e => e.Courses).ThenInclude(e => e.Enrollments).FirstOrDefaultAsync(e => e.Id == instructorId);
            return result ?? throw new InvalidOperationException();
        }

        public async Task<Instructor> GetWithFollowersAsync(int instructorId)
        {
            var result = await context.Instructors.Include(e => e.FollowedBy).ThenInclude(e => e.Student).FirstOrDefaultAsync(e => e.Id == instructorId);
            return result ?? throw new InvalidOperationException();
        }
    }
}
