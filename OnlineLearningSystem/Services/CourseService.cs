using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task AddCourseAsync(AddCourseViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model is null");
            }

            Instructor instructor = await CheckAndGetInstructorAsync(model.InstructorId);

            Course course = new Course()
            {
                Name = model.Name,
                InstructorId = model.InstructorId,
                CategoryId = model.CategoryId,
                CreationDate = DateTime.UtcNow,
                Description = model.Description,
                Price = model.Price
            };

            await unitOfWork.Courses.AddAsync(course);
            await unitOfWork.CompleteAsync();
        }

        private async Task<Instructor> CheckAndGetInstructorAsync(int instructorId)
        {
            Instructor instructor = await unitOfWork.Instructors.GetByIdAsync(instructorId);

            if (instructor == null) { throw new ArgumentNullException($"There is no Instructor with Id = {instructorId}"); }

            return instructor;
        }
    }
}
