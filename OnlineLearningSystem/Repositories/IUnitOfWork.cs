using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IStudentRepository Students { get; }
        public IInstructorRepository Instructors { get; }
        public ICourseRepository Courses { get; }

        public IRepository<Enrollment> Enrollments { get; }

        public IRepository<Lesson> Lessons { get; }

        public IRepository<Category> Categories { get; }

        public IRepository<Like> Likes { get; }

        public IRepository<Follow> Follows { get; }

        public IRepository<Payment> Payments { get; }

        public Task CompleteAsync();
    }
}
