using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly IStudentService studentService;
        private readonly IInstructorService instructorService;
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;

        public AdminService(IStudentService studentService, IInstructorService instructorService, ICourseService courseService, ICategoryService categoryService, IPaymentService paymentService)
        {
            this.studentService = studentService;
            this.instructorService = instructorService;
            this.courseService = courseService;
            this.categoryService = categoryService;
        }

        public async Task<AdminDashboardViewModel> AdminDashboard()
        {
            AdminDashboardViewModel model = new AdminDashboardViewModel();
            model.NumberOfCourses = await courseService.CountCourses();
            model.NumberOfInstructors = await instructorService.CountInstructors();
            model.NumberOfStudent = await studentService.CountStudents();

            return model;
        }

        public async Task<ManageCategoryViewModel> ManageCategories()
        {

            ManageCategoryViewModel model = new ManageCategoryViewModel();

            foreach(var category in await categoryService.GetCategoriesWithCoursesAsync())
            {
                model.Categories.Add(new ShowCategoryViewModel() {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    NumberOfCourses = category.Courses.Count(),
                });
            }
            return model;
        }

        public async Task<ManageCoursesViewModel> ManageCourses()
        {
            ManageCoursesViewModel model = new ManageCoursesViewModel();
            
            foreach(var course in await courseService.GetAllCoursesAsync())
            {
                model.Courses.Add(new ShowCoursesInHomeViewModel() {
                    CourseId = course.CourseId,
                    CategoryName = course.CategoryName,
                    CourseName = course.CourseName,
                    InstructorName = course.InstructorName,
                    NumberOfLessons = course.NumberOfLessons,
                    NumberOfLikes = course.NumberOfLikes,
                    CreatedDate = course.CreatedDate,
                    InstructorId = course.InstructorId
                });
            }

            return model;
        }

        public async Task<ManageInstructorViewModel> ManageInstructors()
        {
            ManageInstructorViewModel model = new ManageInstructorViewModel();

            foreach (var instructor in await instructorService.GetAllInstructorsWithIdentityCourses())
            {
                model.Instructors.Add(new ShowInstructorAdminInforViewModel()
                {
                    InstructorId = instructor.Id,
                    InstructorName = instructor.FirstName + " " + instructor.LastName,
                    InstructorEmail = instructor.IdentityUser.Email,
                    NumberOfCourses= instructor.Courses.Count,
                    PhotoPath = instructor.ProfilePhotoPath
                });
            }
            return model;
        }

        public async Task<ManageStudentViewModel> ManageStudents()
        {
            ManageStudentViewModel model = new ManageStudentViewModel();

            foreach(var student in await studentService.GetAllStudentsWithIdentityEnrollments())
            {
                model.Students.Add(new ShowStudentAdminInfoViewModel() { 
                    StudentId = student.Id,
                    StudentEmail = student.IdentityUser.Email,
                    StudentName = student.FirstName + " " + student.LastName,
                    NumberOfCoursesEnrolled = student.Enrollments.Count,
                    PhotoPath = student.ProfilePhotoPath,
                    Coins = student.Coins
                });
            }
            return model;
        }
    }
}
