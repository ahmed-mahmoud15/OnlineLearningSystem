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
        private ILessonRepository lessonRepository;
        private ICategoryRepository categoryRepository;
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

        public ILessonRepository Lessons => lessonRepository ??= new LessonRepository(context);

        public ICategoryRepository Categories => categoryRepository ??= new CategoryRepository(context);

        public IRepository<Like> Likes => likeRepository ??= new Repository<Like>(context);

        public IRepository<Follow> Follows => followRepository ??= new Repository<Follow>(context);

        public IRepository<Payment> Payments => paymentRepository ??= new Repository<Payment>(context);

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
