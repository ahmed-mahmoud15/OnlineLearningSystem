using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Configurations;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new InstructorConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new EnrollmentConfiguration());
            builder.ApplyConfiguration(new LessonConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new FollowConfiguration());
        }
    }
}
