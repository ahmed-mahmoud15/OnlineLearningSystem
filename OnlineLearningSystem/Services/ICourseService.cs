using OnlineLearningSystem.DTOs;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface ICourseService
    {
        public Task AddCourseAsync(AddCourseViewModel model);
        //public Task GetAllCoursesByCategoryAsync(int categoryId);
        public Task<IEnumerable<ShowCoursesInAdminViewModel>> GetAllCoursesAsync();
        public Task<PaginateResultDTO<ShowCoursesInfoViewModel>> GetAllCoursesPaginationAsync(int count, int page);
        public Task<PaginateResultDTO<ShowCoursesInfoViewModel>> SearchCoursesCoursesPaginationAsync(string searchTerm, int? categoryId);

        public Task<CourseDetailsViewModel> GetCourseDetailsAsync(int courseId);

        public Task<int> CountCourses();
    }
}
