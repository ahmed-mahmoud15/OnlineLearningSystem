using OnlineLearningSystem.Models;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IInstructorService
    {
        public Task EditInstructorAsync(EditInstructorViewModel model);
        public Task<InstructorProfileViewModel> GetInstructorProfileAsync(int id);
        public Task<IEnumerable<ShowInstructorInfoViewModel>> GetInstructorsInfoAsync();
        public Task<EditInstructorViewModel> GetInstructorEditAsync(int instructorId);
        public Task<IEnumerable<Instructor>> GetAllInstructorsWithIdentityCourses();

        public Task<int> CountInstructors();
    }
}
