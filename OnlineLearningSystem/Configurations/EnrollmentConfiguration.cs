using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Date).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.Progress).IsRequired().HasDefaultValue(0);

            builder.HasIndex(e => new {e.StudentId, e.CourseId}).IsUnique();

            builder.HasOne(e => e.Student).WithMany(e => e.Enrollments).HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Course).WithMany(e => e.Enrollments).HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Payment).WithOne().HasForeignKey<Enrollment>(e => e.PaymentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
