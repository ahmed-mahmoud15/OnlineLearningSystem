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

        public Task<bool> CheckIsUserBanned(int userId);

        public Task BanUser(int userId);
        public Task UnbanUser(int userId);
    }
}
