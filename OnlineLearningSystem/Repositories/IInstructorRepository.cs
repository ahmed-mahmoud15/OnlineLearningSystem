using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        public Task<Instructor> GetWithCoursesAsync(int instructorId);
        public Task<Instructor> GetWithFollowersAsync(int instructorId);

        public Task<IEnumerable<Instructor>> GetAllWithCoursesFollowersAsync();

        public Task<IEnumerable<Instructor>> GetAllWithIdentityCoursesAsync();
        public Task<int> GetTotalNumberOfInstructorsAsync();

    }
}
