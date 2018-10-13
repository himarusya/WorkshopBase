using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WorkshopBase.Migrations
{
    public partial class FixDate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Posts_postID",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "postID",
                table: "Workers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Posts_postID",
                table: "Workers",
                column: "postID",
                principalTable: "Posts",
                principalColumn: "postID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Posts_postID",
                table: "Workers");

            migrationBuilder.AlterColumn<int>(
                name: "postID",
                table: "Workers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Posts_postID",
                table: "Workers",
                column: "postID",
                principalTable: "Posts",
                principalColumn: "postID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
