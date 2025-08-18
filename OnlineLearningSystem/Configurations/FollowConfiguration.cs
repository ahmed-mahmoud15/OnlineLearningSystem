using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Configurations
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.InstructorId });
            builder.Property(e => e.FollowDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(e => e.Student).WithMany(e => e.Follows).HasForeignKey(e => e.StudentId);
            builder.HasOne(e => e.Instructor).WithMany(e => e.FollowedBy).HasForeignKey(e => e.InstructorId);
        }
    }
}
