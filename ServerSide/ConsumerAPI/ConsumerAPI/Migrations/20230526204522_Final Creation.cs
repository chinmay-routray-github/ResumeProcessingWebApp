using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FinalCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AllCandidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CandidateName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllCandidates", x => x.CandidateId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AllConsumers",
                columns: table => new
                {
                    ConsumerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConsumerName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllConsumers", x => x.ConsumerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AllJobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConsumerId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    JobDescription = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    KeySkills = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    YearOfExperience = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllJobs", x => x.JobId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AllResumes",
                columns: table => new
                {
                    ResumeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    FileData = table.Column<byte[]>(type: "longblob", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ContentType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendorEmail = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MatchingScore = table.Column<int>(type: "int", nullable: false),
                    SelectionStatus = table.Column<int>(type: "int", nullable: false),
                    AppliedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllResumes", x => x.ResumeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AllVendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VendorName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllVendors", x => x.VendorId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllCandidates");

            migrationBuilder.DropTable(
                name: "AllConsumers");

            migrationBuilder.DropTable(
                name: "AllJobs");

            migrationBuilder.DropTable(
                name: "AllResumes");

            migrationBuilder.DropTable(
                name: "AllVendors");
        }
    }
}
