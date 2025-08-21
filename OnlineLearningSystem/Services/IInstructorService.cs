using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IInstructorService
    {
        public Task EditInstructorAsync(EditInstructorViewModel model);
        public Task<InstructorProfileViewModel> GetInstructorProfileAsync(int id);
        public Task<IEnumerable<ShowInstructorInfoViewModel>> GetInstructorsInfoAsync();
        public Task<EditInstructorViewModel> GetInstructorEditAsync(int instructorId);
    }
}
