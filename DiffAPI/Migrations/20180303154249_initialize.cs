﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiffAPI.Migrations
{
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// DB migration script - responsible for seeding database and creating schema
    /// auto-generated by "dotnet ef migrations add initialize" command
    /// </summary>
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Json",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JsonId = table.Column<string>(nullable: true),
                    Left = table.Column<string>(nullable: true),
                    Right = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Json", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Json");
        }
    }
}
