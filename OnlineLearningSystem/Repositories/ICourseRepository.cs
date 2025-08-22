using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        public Task<Course> GetWithLessonsAsync(int courseId);

        public Task<Course> GetWithLikesAsync(int courseId);

        public Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId);
        
        public Task<IEnumerable<Course>> GetByInstructorIdAsync(int instructorId);

        public Task<IEnumerable<Course>> GetAllWithInstructorCategoryLikesLessonsAsync();
        public Task<Course> GetWithInstructorCategoryLikesLessonsAsync(int courseId);

        public Task<int> GetTotalNumberOfCoursesAsync();
    }
}
