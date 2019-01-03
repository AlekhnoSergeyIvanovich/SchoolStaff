using DAL.Entities;

namespace DAL.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> entity)
        {
            entity.ToTable("Professions");
            entity.HasKey(e => e.Id);
            entity.HasAlternateKey( u => u.NameProfession );
            //entity.Property(c => c.SchoolStaffProfession)
            //    .HasMaxLength(entity.Property(c => c.CountPeopleProfession).);
        }
    }
}