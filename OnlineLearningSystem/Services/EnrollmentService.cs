using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> IsStudentEnrolledInCourseAsync(int studentId, int courseId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);

            Enrollment checkPrevEnrollment = await unitOfWork.Enrollments.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            return checkPrevEnrollment != null;
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

        public async Task<bool> IsStudentCompleteLessonAsync(int studentId, int courseId, int lessonId)
        {
            Enrollment enrollment = await unitOfWork.Enrollments.GetEnrollmentWithStudentAndCourseAsync(studentId, courseId);
            if (enrollment == null) {
                throw new ArgumentNullException("No Enrollment");
            }

            Lesson lesson = await unitOfWork.Lessons.GetByIdAsync(lessonId);

            return enrollment.LastViewedLesson >= lesson.SequenceNumber;
        }

        public async Task<int> CompleteLessonAsync(int studentId, int courseId, int lessonId)
        {
            Enrollment enrollment = await unitOfWork.Enrollments.GetEnrollmentWithStudentAndCourseAsync(studentId, courseId);
            if (enrollment == null)
            {
                throw new ArgumentNullException("No Enrollment");
            }
            Lesson lesson = await unitOfWork.Lessons.GetByIdAsync(lessonId);

            enrollment.LastViewedLesson = lesson.SequenceNumber;
            enrollment.Progress = (float)lesson.SequenceNumber / enrollment.Course.Lessons.Count * 100;
            unitOfWork.Enrollments.Update(enrollment);
            await unitOfWork.CompleteAsync();
            return lesson.SequenceNumber;
        }

        public async Task<IEnumerable<ShowCourseInStudentProfileViewModel>> GetEnrollmentsInfoAsync(int studentId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);

            student = await unitOfWork.Students.GetWithEnrollmentsAsync(studentId);

            return student.Enrollments.Select(e => new ShowCourseInStudentProfileViewModel()
            {
                EnrollDate = e.Date,
                Description = e.Course.Description,
                Id = e.Course.Id,
                Name = e.Course.Name,
                Prgress = e.Progress
            }).ToList();
        }
    }
}
