using OnlineLearningSystem.Models;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IStudentService
    {
        public Task<StudentProfileViewModel> GetStudentProfileAsync(int id);
        public Task EditStudentAsync(EditStudentViewModel model);
        public Task<EditStudentViewModel> GetStudentEditAsync(int studentId);

        public Task<IEnumerable<ShowCourseInStudentProfileViewModel>> GetStudentLikes(int studentId);
        public Task<IEnumerable<ShowInstructorInfoViewModel>> GetStudentFollowing(int studentId);

        public Task<IEnumerable<Student>> GetAllStudentsWithIdentityEnrollments();

        public Task<int> CountStudents();
    }
}
