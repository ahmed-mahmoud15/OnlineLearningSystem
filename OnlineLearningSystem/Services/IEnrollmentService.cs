using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IEnrollmentService
    {
        public Task EnrollInCourse(int studentId, int courseId);
        public Task<bool> IsStudentEnrolledInCourseAsync(int studentId, int courseId);
        public Task<bool> IsStudentCompleteLessonAsync(int studentId, int courseId, int lessonId);
        public Task<int> CompleteLessonAsync(int studentId, int courseId, int lessonId);

        public Task<IEnumerable<ShowCourseInStudentProfileViewModel>> GetEnrollmentsInfoAsync(int studentId);

    }
}
