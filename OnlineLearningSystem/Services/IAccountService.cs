using OnlineLearningSystem.Models;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IAccountService
    {
        public Task RegisterStudent(Student student, IFormFile file);
        public Task RegisterInstructor(CreateInstructorViewModel model);
        public Task RegisterAdmin(User user, IFormFile file);

        public Task<User> GetUserByIdentityIdAsync(string identityId);
    }
}
