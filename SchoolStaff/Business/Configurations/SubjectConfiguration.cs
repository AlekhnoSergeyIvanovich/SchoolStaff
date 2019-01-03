using DAL.Entities;

namespace DAL.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> entity)
        {
            entity.ToTable("Subjects");
            entity.HasKey(e => e.Id);
            entity.HasAlternateKey(u => u.Name);
        }
    }
}