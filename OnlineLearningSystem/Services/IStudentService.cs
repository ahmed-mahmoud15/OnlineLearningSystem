using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Services
{
    public interface IStudentService
    {
        public Task AddStudentAsync(Student student, IFormFile file);
    }
}
