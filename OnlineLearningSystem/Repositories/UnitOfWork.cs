using OnlineLearningSystem.Data;

namespace OnlineLearningSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IUserRepository userRepository;
        private IStudentRepository studentRepository;
        private IInstructorRepository instructorRepository;
        private ICourseRepository courseRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IUserRepository Users => userRepository ??= new UserRepository(context);

        public IStudentRepository Students => throw new NotImplementedException();

        public IInstructorRepository Instructors => throw new NotImplementedException();

        public ICourseRepository Courses => throw new NotImplementedException();

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
