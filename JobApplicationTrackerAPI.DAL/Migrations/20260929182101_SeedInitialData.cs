using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobApplicationTrackerAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "Industry", "Location", "Name", "WebsiteUrl" },
                values: new object[,]
                {
                    { 1, "Software Development", "Winnipeg, MB", "TechCorp", "https://techcorp.example.com" },
                    { 2, "Fintech", "Remote", "Innovate Solutions", "https://innovatesolutions.example.com" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "SkillId", "Category", "Name" },
                values: new object[,]
                {
                    { 1, "Backend", "C#" },
                    { 2, "Backend", ".NET Core" },
                    { 3, "Frontend", "React" },
                    { 4, "Frontend", "TypeScript" },
                    { 5, "Database", "SQL Server" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "alex.morgan@example.com", "Alex", "Morgan", "AQAAAAEAACcQAAAAEHhashedpasswordexample==" });

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "ApplicationId", "AppliedDate", "CompanyId", "JobTitle", "JobUrl", "SalaryMax", "SalaryMin", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Full Stack Developer", "https://techcorp.example.com/careers/123", 95000m, 80000m, "Interviewing", 1 },
                    { 2, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Frontend Engineer", "https://innovatesolutions.example.com/careers/456", 90000m, 75000m, "Applied", 1 }
                });

            migrationBuilder.InsertData(
                table: "ApplicationSkill",
                columns: new[] { "ApplicationId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 5 },
                    { 2, 3 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Interview",
                columns: new[] { "InterviewId", "ApplicationId", "InterviewerName", "IsCompleted", "Notes", "ScheduledAt", "StageName" },
                values: new object[] { 1, 1, "Sarah Jenkins", true, "System design discussion and live coding challenge in C#.", new DateTime(2026, 3, 10, 14, 0, 0, 0, DateTimeKind.Utc), "Technical Screening" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationSkill",
                keyColumns: new[] { "ApplicationId", "SkillId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ApplicationSkill",
                keyColumns: new[] { "ApplicationId", "SkillId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ApplicationSkill",
                keyColumns: new[] { "ApplicationId", "SkillId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "ApplicationSkill",
                keyColumns: new[] { "ApplicationId", "SkillId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ApplicationSkill",
                keyColumns: new[] { "ApplicationId", "SkillId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Interview",
                keyColumn: "InterviewId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "ApplicationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "ApplicationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "SkillId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "SkillId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "SkillId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "SkillId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "SkillId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
