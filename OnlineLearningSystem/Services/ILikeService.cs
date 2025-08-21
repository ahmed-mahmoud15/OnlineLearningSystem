namespace OnlineLearningSystem.Services
{
    public interface ILikeService
    {
        public Task LikeCourse(int studentId, int courseId);
        public Task DislikeCourse(int studentId, int courseId);
    }
}
