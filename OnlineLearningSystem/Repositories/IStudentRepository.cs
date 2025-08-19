using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface IStudentRepository
    {
        public Task<Student> GetStudentWithUserDataAsync(int studentId);
    }
}
