using System.Threading;
using System.Threading.Tasks;
using DAL.Configurations;
using DAL.Entities;
using HTP.Labs.SchoolStaff.Utils.Asynchronism;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<SchoolStaff> SchoolStaffs { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SchoolStaffSubject> SchoolStaffSubjects { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<SchoolStaffProfession> SchoolStaffProfessions { get; set; }
        public virtual DbSet<SchoolStaffPhone> SchoolStaffPhones { get; set; }
        public virtual DbSet<SentMessage> SentMessages { get; set; }

        public event EventHandlerAsync OnUpdateChangesSaved;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        public ApplicationDbContext()
        {
            //Database.EnsureCreated();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new SchoolStaffConfiguration());
            builder.ApplyConfiguration(new SubjectConfiguration());
            builder.ApplyConfiguration(new SchoolStaffSubjectConfiguration());
            builder.ApplyConfiguration(new ProfessionConfiguration());
            builder.ApplyConfiguration(new SchoolStaffProfessionConfiguration());
            builder.ApplyConfiguration(new SchoolStaffPhoneConfiguration());
            builder.ApplyConfiguration(new SentMessagesConfiguration());
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=IVAN-PC\SQLEXPRESS;Initial Catalog=ListOfScoolStaff;Integrated Security=True;");
        }

        public async Task<EntityEntry> UpdateChangesSaved(object entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mUpdate = base.Update(entity);
            EventArgsForAsyncHandler a = new EventArgsForAsyncHandler(cancellationToken);
            await OnUpdateChangesSaved?.Invoke(
                this, a);

            return mUpdate;
        }

        public void RemoveChangesSaved(object entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            base.Remove(entity);
            var a = new EventArgsForAsyncHandler(cancellationToken);
        }
    }
}