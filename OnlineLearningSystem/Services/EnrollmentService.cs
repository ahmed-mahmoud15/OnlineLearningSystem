using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task EnrollInCourse(int studentId, int courseId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);

            Enrollment checkPrevEnrollment = await unitOfWork.Enrollments.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (checkPrevEnrollment != null)
            {
                throw new InvalidOperationException("This student already enrolled in this course before");
            }

            if (student.Coins < course.Price)
            {
                throw new InvalidOperationException("Student doesn't have enough Coins to proceed in this enrollment");
            }

            Enrollment enrollment = new Enrollment()
            {
                CourseId = courseId,
                StudentId = studentId,
                Date = DateTime.UtcNow,
                Progress = 0,
                LastViewedLesson = 0
            };

            student.Coins -= course.Price;


            unitOfWork.Students.Update(student);
            await unitOfWork.Enrollments.AddAsync(enrollment);

            await unitOfWork.CompleteAsync();
        }
    }
}
