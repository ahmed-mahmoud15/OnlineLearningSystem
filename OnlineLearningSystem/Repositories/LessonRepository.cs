using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Lesson> GetLessonWithCourse(int lessonId)
        {
            return await context.Lessons.Include(e => e.Course).FirstOrDefaultAsync(e => e.Id == lessonId);
        }
    }
}
