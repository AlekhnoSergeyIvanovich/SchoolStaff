using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    using Microsoft.EntityFrameworkCore;

    public class SchoolStaffPhoneConfiguration : IEntityTypeConfiguration<SchoolStaffPhone>
    {
        public void Configure(EntityTypeBuilder<SchoolStaffPhone> entity)
        {
            entity.ToTable("SchoolStaffPhones");
            entity.HasKey(e => e.Id);

            /*
            entity.HasOne(sc => sc.SchoolStaffPhoneOne)
                .WithOne(s => s.SsPhone)
                .HasForeignKey<SchoolStaff>(c => c.PrimaryPhoneId);
                //.OnDelete(DeleteBehavior.SetNull);
            */

            entity.HasOne(sc => sc.SchoolStaff)
                .WithMany(s => s.SchoolStaffPhones)
                .HasForeignKey(sc => sc.SchoolStaffId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
