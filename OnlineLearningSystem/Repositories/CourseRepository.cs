using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetAllWithInstructorCategoryLikesLessonsAsync()
        {
            return await context.Courses.Include(e => e.Category).Include(e => e.Instructor).Include(e => e.LikedBy).Include(e => e.Enrollments).Include(e => e.Lessons).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId)
        {
            return await context.Courses.Where(e => e.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByInstructorIdAsync(int instructorId)
        {
            return await context.Courses.Where(e => e.InstructorId == instructorId).ToListAsync();
        }

        public async Task<int> GetTotalNumberOfCoursesAsync()
        {
            return await context.Courses.CountAsync();
        }

        public async Task<Course> GetWithInstructorCategoryLikesLessonsAsync(int courseId)
        {
            return await context.Courses.Include(e => e.Category).Include(e => e.Instructor).Include(e => e.LikedBy).Include(e => e.Lessons).Include(e => e.Enrollments).FirstOrDefaultAsync(e => e.Id == courseId);
        }

        public async Task<Course> GetWithLessonsAsync(int courseId)
        {
            var result = await context.Courses.Include(e => e.Lessons).FirstOrDefaultAsync(e => e.Id == courseId);
            return result ?? throw new InvalidOperationException();
        }

        public async Task<Course> GetWithLikesAsync(int courseId)
        {
            var result = await context.Courses.Include(e => e.LikedBy).ThenInclude(e => e.Student).FirstOrDefaultAsync(e => e.Id == courseId);
            return result ?? throw new InvalidOperationException();
        }
    }
}
