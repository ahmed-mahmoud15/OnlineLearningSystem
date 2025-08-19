using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Description).IsRequired(false);

            builder.Property(e => e.Price).IsRequired();

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Course_Price", "[Price] >= 0"));

            builder.Property(e => e.CreationDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(e => e.Instructor).WithMany(e => e.Courses).HasForeignKey(e => e.InstructorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Category).WithMany(e => e.Courses).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Lessons).WithOne(e => e.Course).HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
