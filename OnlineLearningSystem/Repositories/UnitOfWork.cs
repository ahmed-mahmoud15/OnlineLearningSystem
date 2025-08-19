using OnlineLearningSystem.Data;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IUserRepository userRepository;
        private IStudentRepository studentRepository;
        private IInstructorRepository instructorRepository;
        private ICourseRepository courseRepository;
        private IRepository<Enrollment> enrollmentRepository;
        private IRepository<Lesson> lessonRepository;
        private IRepository<Category> categoryRepository;
        private IRepository<Like> likeRepository;
        private IRepository<Follow> followRepository;
        private IRepository<Payment> paymentRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IUserRepository Users => userRepository ??= new UserRepository(context);

        public IStudentRepository Students => studentRepository ??= new StudentRepository(context);

        public IInstructorRepository Instructors => instructorRepository ??= new InstructorRepository(context);

        public ICourseRepository Courses => courseRepository ??= new CourseRepository(context);

        public IRepository<Enrollment> Enrollments => enrollmentRepository ??= new Repository<Enrollment>(context);

        public IRepository<Lesson> Lessons => lessonRepository ??= new Repository<Lesson>(context);

        public IRepository<Category> Categories => categoryRepository ??= new Repository<Category>(context);

        public IRepository<Like> Likes => likeRepository ??= new Repository<Like>(context);

        public IRepository<Follow> Follows => followRepository ??= new Repository<Follow>(context);

        public IRepository<Payment> Payments => paymentRepository ??= new Repository<Payment>(context);

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
