using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WorshopBase.Migrations
{
    public partial class WorkshopB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    mechanicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    experience = table.Column<int>(nullable: false),
                    fioMechanic = table.Column<string>(nullable: true),
                    qualification = table.Column<string>(nullable: true),
                    salary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.mechanicID);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    ownerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    adress = table.Column<string>(nullable: true),
                    driverLicense = table.Column<int>(nullable: false),
                    fioOwner = table.Column<string>(nullable: true),
                    phone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.ownerID);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    carID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bodyNumber = table.Column<int>(nullable: false),
                    colour = table.Column<string>(nullable: true),
                    engineNumber = table.Column<int>(nullable: false),
                    model = table.Column<string>(nullable: true),
                    ownerID = table.Column<int>(nullable: false),
                    vis = table.Column<int>(nullable: false),
                    yearOfIssue = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.carID);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_ownerID",
                        column: x => x.ownerID,
                        principalTable: "Owners",
                        principalColumn: "ownerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workrooms",
                columns: table => new
                {
                    workroomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    carID = table.Column<int>(nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    mechanicID = table.Column<int>(nullable: false),
                    receiptDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workrooms", x => x.workroomID);
                    table.ForeignKey(
                        name: "FK_Workrooms_Cars_carID",
                        column: x => x.carID,
                        principalTable: "Cars",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workrooms_Mechanics_mechanicID",
                        column: x => x.mechanicID,
                        principalTable: "Mechanics",
                        principalColumn: "mechanicID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ownerID",
                table: "Cars",
                column: "ownerID");

            migrationBuilder.CreateIndex(
                name: "IX_Workrooms_carID",
                table: "Workrooms",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_Workrooms_mechanicID",
                table: "Workrooms",
                column: "mechanicID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workrooms");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Mechanics");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
