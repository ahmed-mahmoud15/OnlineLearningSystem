namespace OnlineLearningSystem.Services
{
    public interface IFollowService
    {
        public Task FollowInstructor(int studentId, int instructorId);
        public Task UnfollowInstructor(int studentId, int instructorId);

        public Task<bool> IsStudentFollowingInstructor(int studentId, int instructorId);
    }
}
