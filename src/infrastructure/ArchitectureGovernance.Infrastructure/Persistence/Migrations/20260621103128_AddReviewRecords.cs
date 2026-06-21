using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchitectureGovernance.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtifactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequirementSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewRecords_ArchitectureProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ArchitectureProjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReviewRecords_GeneratedArtifacts_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "GeneratedArtifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewRecords_Requirements_RequirementSubmissionId",
                        column: x => x.RequirementSubmissionId,
                        principalTable: "Requirements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRecords_ArtifactId",
                table: "ReviewRecords",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRecords_ProjectId",
                table: "ReviewRecords",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRecords_RequirementSubmissionId",
                table: "ReviewRecords",
                column: "RequirementSubmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewRecords");
        }
    }
}
