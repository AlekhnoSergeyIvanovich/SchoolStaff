using DAL.Entities;

namespace DAL.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SchoolStaffProfessionConfiguration : IEntityTypeConfiguration<SchoolStaffProfession>
    {
        public void Configure(EntityTypeBuilder<SchoolStaffProfession> entity)
        {
            entity.ToTable("SchoolStaffProfessions");
            entity.HasKey(e => e.Id);

            entity.HasOne(sc => sc.SchoolStaff)
                .WithMany(s => s.SchoolStaffProfession)
                .HasForeignKey(sc => sc.SchoolStaffId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sc => sc.Profession)
                .WithMany(c => c.SchoolStaffProfession)
                .HasForeignKey(sc => sc.ProfessionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasAlternateKey(u => new { u.SchoolStaffId, u.ProfessionId });
        }
    }
}