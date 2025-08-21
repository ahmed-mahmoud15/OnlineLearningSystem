using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork unitOfWork;

        public FollowService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task FollowInstructor(int studentId, int instructorId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(instructorId, unitOfWork);

            Follow oldFollow = await unitOfWork.Follows.GetWithConditionAsync(e => e.StudentId == studentId && e.InstructorId == instructorId);

            if (oldFollow != null)
            {
                throw new InvalidOperationException("This student is already following this instructor");
            }

            Follow follow = new Follow()
            {
                FollowDate = DateTime.UtcNow,
                InstructorId = instructorId,
                StudentId = studentId
            };

            await unitOfWork.Follows.AddAsync(follow);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> IsStudentFollowingInstructor(int studentId, int instructorId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(instructorId, unitOfWork);

            Follow oldFollow = await unitOfWork.Follows.GetWithConditionAsync(e => e.StudentId == studentId && e.InstructorId == instructorId);

            return oldFollow != null;
        }

        public async Task UnfollowInstructor(int studentId, int instructorId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(instructorId, unitOfWork);

            Follow oldFollow = await unitOfWork.Follows.GetWithConditionAsync(e => e.StudentId == studentId && e.InstructorId == instructorId);

            if (oldFollow == null)
            {
                throw new InvalidOperationException("This student is not following this instructor");
            }

            unitOfWork.Follows.DeleteObject(oldFollow);
            await unitOfWork.CompleteAsync();
        }
    }
}
