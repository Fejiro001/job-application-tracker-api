using JobApplicationTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.DAL
{
    public static class DbInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // 1. Seed Skills
            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 1, Name = "C#", Category = "Backend" },
                new Skill { SkillId = 2, Name = ".NET Core", Category = "Backend" },
                new Skill { SkillId = 3, Name = "React", Category = "Frontend" },
                new Skill { SkillId = 4, Name = "TypeScript", Category = "Frontend" },
                new Skill { SkillId = 5, Name = "SQL Server", Category = "Database" }
            );

            // 2. Seed User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Alex",
                    LastName = "Morgan",
                    Email = "alex.morgan@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEHhashedpasswordexample==", // Placeholder hash
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // 3. Seed Companies
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    CompanyId = 1,
                    Name = "TechCorp",
                    WebsiteUrl = "https://techcorp.example.com",
                    Industry = "Software Development",
                    Location = "Winnipeg, MB"
                },
                new Company
                {
                    CompanyId = 2,
                    Name = "Innovate Solutions",
                    WebsiteUrl = "https://innovatesolutions.example.com",
                    Industry = "Fintech",
                    Location = "Remote"
                }
            );

            // 4. Seed Applications
            modelBuilder.Entity<Application>().HasData(
                new Application
                {
                    ApplicationId = 1,
                    UserId = 1,
                    CompanyId = 1,
                    JobTitle = "Full Stack Developer",
                    JobUrl = "https://techcorp.example.com/careers/123",
                    Status = Status.Interviewing,
                    AppliedDate = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    SalaryMin = 80000m,
                    SalaryMax = 95000m
                },
                new Application
                {
                    ApplicationId = 2,
                    UserId = 1,
                    CompanyId = 2,
                    JobTitle = "Frontend Engineer",
                    JobUrl = "https://innovatesolutions.example.com/careers/456",
                    Status = Status.Applied,
                    AppliedDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                    SalaryMin = 75000m,
                    SalaryMax = 90000m
                }
            );

            // 5. Seed Interviews
            modelBuilder.Entity<Interview>().HasData(
                new Interview
                {
                    InterviewId = 1,
                    ApplicationId = 1,
                    StageName = "Technical Screening",
                    InterviewerName = "Sarah Jenkins",
                    Notes = "System design discussion and live coding challenge in C#.",
                    IsCompleted = true,
                    ScheduledAt = new DateTime(2026, 3, 10, 14, 0, 0, DateTimeKind.Utc)
                }
            );

            // 6. Seed M:N Join Table (ApplicationSkill)
            modelBuilder.Entity("ApplicationSkill").HasData(
                new { ApplicationId = 1, SkillId = 1 }, // Application 1 uses C#
                new { ApplicationId = 1, SkillId = 2 }, // Application 1 uses .NET Core
                new { ApplicationId = 1, SkillId = 5 }, // Application 1 uses SQL Server
                new { ApplicationId = 2, SkillId = 3 }, // Application 2 uses React
                new { ApplicationId = 2, SkillId = 4 }  // Application 2 uses TypeScript
            );
        }
    }
}
