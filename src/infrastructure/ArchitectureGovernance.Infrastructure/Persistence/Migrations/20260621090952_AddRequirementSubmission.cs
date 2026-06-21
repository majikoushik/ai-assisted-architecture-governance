using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchitectureGovernance.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRequirementSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequirementText = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    BusinessDomain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DomainContext = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubmittedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirements_ArchitectureProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ArchitectureProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_ProjectId",
                table: "Requirements",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requirements");
        }
    }
}
