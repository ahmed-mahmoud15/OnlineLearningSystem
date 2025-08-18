using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(e => new {e.StudentId, e.CourseId});

            builder.Property(e => e.LikedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(e => e.Student).WithMany(e => e.Likes).HasForeignKey(e => e.StudentId);

            builder.HasOne(e => e.Course).WithMany(e => e.LikedBy).HasForeignKey(e => e.CourseId);
        }
    }
}
