using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByIdentityId(string identityId);
    }
}
