using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IInstructorService
    {
        public Task EditInstructorAsync(EditInstructorViewModel model);
    }
}
