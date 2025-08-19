using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class InstructorServic : IInstructorService
    {

        private readonly IUnitOfWork unitOfWork;
        public InstructorServic(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
        }
    }
}
