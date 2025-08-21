using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;
using OnlineLearningSystem.Common_Functionalities;
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

            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(model.InstructorId, unitOfWork);

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

        public async Task<IEnumerable<ShowCoursesInHomeViewModel>> GetAllCoursesAsync()
        {
            var courses = await unitOfWork.Courses.GetAllWithInstructorCategoryLikesAsync();

            IList<ShowCoursesInHomeViewModel> model = courses.Select(e => new ShowCoursesInHomeViewModel() {
                CourseId = e.Id,
                CategoryName = e.Category.Name,
                CourseName = e.Name,
                InstructorId = e.Instructor.Id,
                InstructorName = e.Instructor.FirstName + " " + e.Instructor.LastName,
                NumberOfLessons = e.Lessons.Count,
                NumberOfLiks = e.LikedBy.Count
            }).ToList();

            return model;
        }

        public async Task<CourseDetailsViewModel> GetCourseDetailsAsync(int courseId)
        {
            Course course = await unitOfWork.Courses.GetWithInstructorCategoryLikesAsync(courseId);

            CourseDetailsViewModel model = new CourseDetailsViewModel()
            {
                CategoryName = course.Category.Name,
                CourseName = course.Name,
                CourseId = courseId,
                Price = course.Price,
                CountEnrolledStudents = course.Enrollments.Count,
                CountLessons = course.Lessons.Count,
                CountLikes = course.LikedBy.Count,
                CourseDescription = course.Description,
                InstructorId = course.Instructor.Id,
                InstructorName = course.Instructor.FirstName + " " + course.Instructor.LastName,
                Lessons = course.Lessons.Select(e => new ShowLessonCourseDetailsViewModel()
                {
                    LessonTitle = e.Title,
                    LessonType = e.Type.ToString(),
                    SequenceNumber = e.SequenceNumber
                })
            };

            return model;
        }
    }
}
