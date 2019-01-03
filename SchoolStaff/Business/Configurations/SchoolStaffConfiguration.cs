using DAL.Entities;

namespace DAL.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SchoolStaffConfiguration : IEntityTypeConfiguration<SchoolStaff>
    {
        public void Configure(EntityTypeBuilder<SchoolStaff> entity)
        {
            entity.ToTable("SchoolStaffs");
            entity.HasKey(e => e.Id);

            entity.HasOne(sc => sc.SsPhone)
                .WithOne(s => s.SchoolStaffPhoneOne)
                .IsRequired(false)
                .HasForeignKey<SchoolStaff>(c => c.PrimaryPhoneId)
                .HasPrincipalKey<SchoolStaffPhone>(e => e.Id);
                //.OnDelete(DeleteBehavior.SetNull);

            entity.Property(b => b.typeMessage)
                .HasDefaultValue(TypeMessage.Email);
        }
    }
}