using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork unitOfWork;

        public LikeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task LikeCourse(int studentId, int courseId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);

            Like oldLike = await unitOfWork.Likes.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (oldLike != null)
            {
                throw new InvalidOperationException("This student liked this course before");
            }

            Like like = new Like()
            {
                CourseId = courseId,
                StudentId = studentId,
                LikedDate = DateTime.UtcNow
            };

            await unitOfWork.Likes.AddAsync(like);
            await unitOfWork.CompleteAsync();
        }

        public async Task DislikeCourse(int studentId, int courseId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);

            Like oldLike = await unitOfWork.Likes.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (oldLike == null)
            {
                throw new InvalidOperationException("This student didn't like this course before");
            }

            unitOfWork.Likes.DeleteObject(oldLike);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> IsStudentLikedCourse(int studentId, int courseId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);

            Like oldLike = await unitOfWork.Likes.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            return oldLike != null;
        }
    }
}
