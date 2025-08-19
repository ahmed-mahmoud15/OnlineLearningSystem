using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).IsRequired();

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Payment_Amount", "[Amount] > 0"));

            builder.HasOne(e => e.Student).WithMany(e => e.Payments).HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
