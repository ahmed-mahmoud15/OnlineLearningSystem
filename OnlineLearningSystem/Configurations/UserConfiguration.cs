using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(20);

            builder.Property(u => u.LastName).IsRequired().HasMaxLength(20);

            builder.Property(u => u.Bio).IsRequired(false);

            builder.Property(u => u.ProfilePhotoPath).IsRequired(false);

            builder.Property(u => u.BirthDate).IsRequired();

            builder.HasOne(u => u.IdentityUser).WithOne().HasForeignKey<User>(u => u.IdentityId).IsRequired();
       }
    }
}
