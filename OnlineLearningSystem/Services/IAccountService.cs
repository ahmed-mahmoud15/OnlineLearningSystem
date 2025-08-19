using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Services
{
    public interface IAccountService
    {
        public Task RegisterStudent(Student student, IFormFile file);
        public Task RegisterInstructor(Instructor instructor, IFormFile file);
        public Task RegisterAdmin(User user, IFormFile file);

        public Task<User> GetUserByIdentityIdAsync(string identityId);
    }
}
