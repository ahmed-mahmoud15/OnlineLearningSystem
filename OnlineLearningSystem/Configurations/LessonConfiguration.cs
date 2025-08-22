using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new {e.CourseId, e.SequenceNumber}).IsUnique();

            builder.Property(e => e.Title).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Description).IsRequired(false);

            builder.Property(e => e.FilePath).IsRequired();

            builder.Property(e => e.Type).HasConversion<int>().IsRequired();

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Lesson_Sequence", "[SequenceNumber] > 0"));

        }
    }
}
