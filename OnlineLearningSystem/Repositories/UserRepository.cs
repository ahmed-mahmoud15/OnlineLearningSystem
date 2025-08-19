using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<User> GetByIdentityId(string identityId)
        {
            var user = context.AppUsers.FirstOrDefaultAsync(e => e.IdentityId.Equals(identityId));
            return user ?? throw new ArgumentException("Invalid Identity Id");
        }
    }
}
