using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_UserId",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_ProjectId1",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_ProjectId1",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_projects_UserId",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "projects");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_ProjectId1",
                table: "tasks",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_projects_UserId",
                table: "projects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_UserId",
                table: "projects",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_ProjectId1",
                table: "tasks",
                column: "ProjectId1",
                principalTable: "projects",
                principalColumn: "Id");
        }
    }
}
