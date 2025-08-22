using Microsoft.AspNetCore.Cors.Infrastructure;
using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.DTOs;
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

        public async Task<int> CountCourses()
        {
            return await unitOfWork.Courses.GetTotalNumberOfCoursesAsync();
        }

        public async Task<IEnumerable<ShowCoursesInAdminViewModel>> GetAllCoursesAsync()
        {
            var courses = await unitOfWork.Courses.GetAllWithInstructorCategoryLikesAsync();

            IList<ShowCoursesInAdminViewModel> model = courses.Select(e => new ShowCoursesInAdminViewModel() {
                CourseId = e.Id,
                CategoryName = e.Category.Name,
                CourseName = e.Name,
                InstructorId = e.Instructor.Id,
                InstructorName = e.Instructor.FirstName + " " + e.Instructor.LastName,
                NumberOfLessons = e.Lessons.Count,
                NumberOfLikes = e.LikedBy.Count,
                CreatedDate = e.CreationDate
            }).ToList();

            return model;
        }

        public async Task<PaginateResultDTO<ShowCoursesInfoViewModel>> GetAllCoursesPaginationAsync(int count, int page)
        {
            var data = await unitOfWork.Courses.GetAllPaginationAsync(page, count, c => c.Instructor, c => c.Enrollments, c => c.Category, c => c.LikedBy, c => c.Lessons);

            IList<ShowCoursesInfoViewModel> model = new List<ShowCoursesInfoViewModel>();

            foreach (var item in data.Items) {
                model.Add(new ShowCoursesInfoViewModel() {
                    CategoryName = item.Category.Name,
                    CourseId = item.Id,
                    CourseName = item.Name,
                    CreatedDate = item.CreationDate,
                    InstructorId = item.Instructor.Id,
                    InstructorName = item.Instructor.FirstName + " " + item.Instructor.LastName,
                    NumberOfLessons = item.Lessons.Count,
                    NumberOfLikes = item.LikedBy.Count,
                    Price = item.Price
                });
            }

            return new PaginateResultDTO<ShowCoursesInfoViewModel>() {
                Items = model,
                PageNumber = data.PageNumber,
                PageSize = data.PageSize,
                TotalCount = data.TotalCount
            };
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

        public async Task<PaginateResultDTO<ShowCoursesInfoViewModel>> SearchCoursesCoursesPaginationAsync(string searchTerm, int? categoryId)
        {
            var courses = await unitOfWork.Courses.GetAllPaginationAsync(
                pageNumber: 1,
                pageSize: 20,
                c => c.Instructor,
                c => c.Enrollments,
                c => c.Category,
                c => c.LikedBy,
                c => c.Lessons
            );

            var query = courses.Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowerSearch = searchTerm.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(lowerSearch));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.Category.Id == categoryId.Value);
            }

            var items = query.Select(item => new ShowCoursesInfoViewModel
            {
                CategoryId = item.Category.Id,
                CategoryName = item.Category.Name,
                CourseId = item.Id,
                CourseName = item.Name,
                CreatedDate = item.CreationDate,
                InstructorId = item.Instructor.Id,
                InstructorName = item.Instructor.FirstName + " " + item.Instructor.LastName,
                NumberOfLessons = item.Lessons.Count,
                NumberOfLikes = item.LikedBy.Count,
                Price = item.Price
            }).ToList();

            return new PaginateResultDTO<ShowCoursesInfoViewModel>
            {
                Items = items,
                PageNumber = 1,
                PageSize = items.Count == 0 ? 1 : items.Count,
                TotalCount = items.Count
            };
        }


    }
}
