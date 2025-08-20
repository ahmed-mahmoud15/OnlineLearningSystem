using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface ICourseService
    {
        public Task AddCourseAsync(AddCourseViewModel model);
    }
}
