using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        public Task<Lesson> GetLessonWithCourse(int lessonId);
    }
}
