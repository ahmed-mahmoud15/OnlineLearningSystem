namespace OnlineLearningSystem.Services
{
    public interface IEnrollmentService
    {
        public Task EnrollInCourse(int studentId, int courseId);
        public Task<bool> IsStudentEnrolledInCourseAsync(int studentId, int courseId);
    }
}
