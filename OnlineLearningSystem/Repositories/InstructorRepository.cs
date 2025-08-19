using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
