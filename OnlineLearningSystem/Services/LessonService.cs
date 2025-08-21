using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork unitOfWork;

        public LessonService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
