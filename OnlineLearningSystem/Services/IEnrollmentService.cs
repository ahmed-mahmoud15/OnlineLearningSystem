namespace OnlineLearningSystem.Services
{
    public interface IEnrollmentService
    {
        public Task EnrollInCourse(int studentId, int courseId);
    }
}
