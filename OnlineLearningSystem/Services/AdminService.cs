using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
