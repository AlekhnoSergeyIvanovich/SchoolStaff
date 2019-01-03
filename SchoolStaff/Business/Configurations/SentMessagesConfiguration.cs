using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class SentMessagesConfiguration : IEntityTypeConfiguration<SentMessage>
    {
        public void Configure(EntityTypeBuilder<SentMessage> entity)
        {
            entity.ToTable("SentMessages");
            entity.HasKey(e => e.Id);

            entity.HasOne(sc => sc.SchoolStaff)
                .WithMany(s => s.SentMessage)
                .HasForeignKey(sc => sc.IdSchoolStaff);
            //.OnDelete(DeleteBehavior.Cascade);

            //entity.Property<DateTime>(: "TimeWrite", defaultValueSql: "GETUTCDATE()");
        }
    }
}