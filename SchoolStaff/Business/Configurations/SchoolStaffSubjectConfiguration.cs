using DAL.Entities;

namespace DAL.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SchoolStaffSubjectConfiguration : IEntityTypeConfiguration<SchoolStaffSubject>
    {
        public void Configure(EntityTypeBuilder<SchoolStaffSubject> entity)
        {
            entity.ToTable("SchoolStaffSubjects");
            entity.HasKey(e => e.Id);

            entity.HasOne(sc => sc.SchoolStaff)
                .WithMany(s => s.SchoolStaffSubjects)
                .HasForeignKey(sc => sc.SchoolStaffId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sc => sc.Subject)
                .WithMany(c => c.SchoolStaffSubjects)
                .HasForeignKey(sc => sc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasAlternateKey(u => new { u.SchoolStaffId, u.SubjectId });
        }
    }
}