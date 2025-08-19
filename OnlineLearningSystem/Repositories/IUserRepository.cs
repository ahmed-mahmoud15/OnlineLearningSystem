using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetByIdentityId(string identityId);
    }
}
