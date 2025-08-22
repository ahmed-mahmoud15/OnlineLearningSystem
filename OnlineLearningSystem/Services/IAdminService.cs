using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IAdminService
    {
        public Task<ManageCategoryViewModel> ManageCategories();
        public Task<ManageStudentViewModel> ManageStudents();
        public Task<ManageInstructorViewModel> ManageInstructors();
        public Task<ManageCoursesViewModel> ManageCourses();

        public Task<AdminDashboardViewModel> AdminDashboard();
        
    }
}
