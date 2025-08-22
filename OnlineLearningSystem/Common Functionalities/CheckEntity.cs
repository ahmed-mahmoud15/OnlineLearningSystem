using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Common_Functionalities
{
    public class CheckEntity
    {

        public static async Task<Student> CheckAndGetStudentAsync(int studentId, IUnitOfWork unitOfWork)
        {
            Student student = await unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {studentId}"); }

            return student;
        }

        public static async Task<User> CheckAndGetUserAsync(int userId, IUnitOfWork unitOfWork)
        {
            User user = await unitOfWork.Users.GetByIdAsync(userId);

            if (user == null) { throw new ArgumentNullException($"There is no User with Id = {userId}"); }

            return user;
        }

        public static async Task<Category> CheckAndGetCategoryAsync(int categoryId, IUnitOfWork unitOfWork)
        {
            Category category = await unitOfWork.Categories.GetCategoryWithCourse(categoryId);

            if (category == null) { throw new ArgumentNullException($"There is no Category with Id = {categoryId}"); }

            return category;
        }

        public static async Task<Course> CheckAndGetCourseAsync(int courseId, IUnitOfWork unitOfWork)
        {
            Course course = await unitOfWork.Courses.GetByIdAsync(courseId);

            if (course == null) { throw new ArgumentNullException($"There is no Course with Id = {courseId}"); }

            return course;
        }

        public static async Task<Instructor> CheckAndGetInstructorAsync(int instructorId, IUnitOfWork unitOfWork)
        {
            Instructor instructor = await unitOfWork.Instructors.GetByIdAsync(instructorId);

            if (instructor == null) { throw new ArgumentNullException($"There is no Instructor with Id = {instructorId}"); }

            return instructor;
        }
    }
}
