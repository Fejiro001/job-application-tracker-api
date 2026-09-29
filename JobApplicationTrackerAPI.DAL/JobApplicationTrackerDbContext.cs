using JobApplicationTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.DAL
{
    public class JobApplicationTrackerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Application> Applications { get; set; }
        public JobApplicationTrackerDbContext(DbContextOptions<JobApplicationTrackerDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== Primary Keys =====
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<Company>()
                .HasKey(c => c.CompanyId);
            modelBuilder.Entity<Interview>()
                .HasKey(i => i.InterviewId);
            modelBuilder.Entity<Skill>()
                .HasKey(s => s.SkillId);
            modelBuilder.Entity<Application>()
                .HasKey(a => a.ApplicationId);

            // ===== Properties =====
            modelBuilder.Entity<User>(entity =>
            {
                // Not use EF's default pluralization
                entity.ToTable("User");

                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.CreatedAt).IsRequired();

                // Constraints
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.WebsiteUrl).IsRequired().HasMaxLength(255);
                entity.Property(c => c.Industry).IsRequired().HasMaxLength(255);
                entity.Property(c => c.Location).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.ToTable("Interview");

                entity.Property(i => i.StageName).IsRequired().HasMaxLength(100);
                entity.Property(i => i.InterviewerName).HasMaxLength(100);
                entity.Property(i => i.Notes);
                entity.Property(i => i.IsCompleted).IsRequired().HasDefaultValue(false);
                entity.Property(i => i.ScheduledAt).IsRequired();
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Category).IsRequired().HasMaxLength(100);

                // Constraints
                entity.HasIndex(s => s.Name).IsUnique();
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application");

                entity.Property(a => a.JobTitle).IsRequired().HasMaxLength(255);
                entity.Property(a => a.JobUrl).IsRequired().HasMaxLength(2048);
                entity.Property(a => a.Status).IsRequired().HasConversion<string>(); // store enum values as strings
                entity.Property(a => a.AppliedDate).IsRequired();
                entity.Property(a => a.SalaryMin).HasPrecision(18, 2);
                entity.Property(a => a.SalaryMax).HasPrecision(18, 2);
            });

            // --- Relationships ---
            // 1:N User to Application
            modelBuilder.Entity<User>()
                .HasMany(u => u.Applications)
                .WithOne(a => a.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // 1:N Company to Application
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Applications)
                .WithOne(a => a.Company)
                .HasForeignKey(t => t.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
            // 1:N Application to Interview
            modelBuilder.Entity<Application>()
                .HasMany(u => u.Interviews)
                .WithOne(a => a.Application)
                .HasForeignKey(t => t.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
            // M:N Application to Skill
            modelBuilder.Entity<Application>()
                .HasMany(a => a.Skills)
                .WithMany(s => s.Applications)
                .UsingEntity("ApplicationSkill",
                    l => l.HasOne(typeof(Skill)).WithMany().HasForeignKey("SkillId"),
                    r => r.HasOne(typeof(Application)).WithMany().HasForeignKey("ApplicationId")
                );
        }
    }
}
