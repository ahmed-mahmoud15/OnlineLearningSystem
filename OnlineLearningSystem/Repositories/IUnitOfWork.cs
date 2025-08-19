namespace OnlineLearningSystem.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IStudentRepository Students { get; }
        public IInstructorRepository Instructors { get; }
        public ICourseRepository Courses { get; }

        public Task CompleteAsync();
    }
}
