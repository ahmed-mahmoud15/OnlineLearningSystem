using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IStudentService studentService;
        private readonly IUserRepository userRepository;

        public AccountService(IStudentService studentService, IUserRepository userRepository)
        {
            this.studentService = studentService;
            this.userRepository = userRepository;
        }

        public Task<User> GetUserByIdentityIdAsync(string identityId)
        {
            return userRepository.GetByIdentityId(identityId);
        }

        public Task RegisterAdmin(User user, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task RegisterInstructor(Instructor instructor, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterStudent(Student student, IFormFile file)
        {
            await studentService.AddStudentAsync(student, file);
        }
    }
}
