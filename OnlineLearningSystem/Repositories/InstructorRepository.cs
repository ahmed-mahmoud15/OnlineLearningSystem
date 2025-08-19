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

        public async Task<Instructor> GetWithCoursesAsync(int instructorId)
        {
            return await context.Instructors.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == instructorId);
        }

        public async Task<Instructor> GetWithFollowersAsync(int instructorId)
        {
            return await context.Instructors.Include(e => e.FollowedBy).ThenInclude(e => e.Student).FirstOrDefaultAsync(e => e.Id == instructorId);
        }
    }
}
