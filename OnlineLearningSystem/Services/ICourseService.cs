using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface ICourseService
    {
        public Task AddCourseAsync(AddCourseViewModel model);
        //public Task GetAllCoursesByCategoryAsync(int categoryId);
        public Task<IEnumerable<ShowCoursesInHomeViewModel>> GetAllCoursesAsync();
        public Task<CourseDetailsViewModel> GetCourseDetailsAsync(int courseId);

        public Task<int> CountCourses();
    }
}
