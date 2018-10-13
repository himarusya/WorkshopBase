using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WorkshopBase.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Parts",
                columns: table => new
                {
                    partID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descriptionPart = table.Column<string>(nullable: true),
                    partName = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.partID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    postID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descriptionPost = table.Column<string>(nullable: true),
                    postName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.postID);
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
                    stateNumber = table.Column<string>(nullable: true),
                    vis = table.Column<int>(nullable: false),
                    yearOfIssue = table.Column<int>(nullable: false)
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
                name: "Workers",
                columns: table => new
                {
                    workerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateOfDismissal = table.Column<DateTime>(nullable: false),
                    dateOfEmployment = table.Column<DateTime>(nullable: false),
                    fioWorker = table.Column<string>(nullable: true),
                    postID = table.Column<int>(nullable: false),
                    salary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.workerID);
                    table.ForeignKey(
                        name: "FK_Workers_Posts_postID",
                        column: x => x.postID,
                        principalTable: "Posts",
                        principalColumn: "postID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    carID = table.Column<int>(nullable: false),
                    dateCompletion = table.Column<DateTime>(nullable: false),
                    dateReceipt = table.Column<DateTime>(nullable: false),
                    workerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Orders_Cars_carID",
                        column: x => x.carID,
                        principalTable: "Cars",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Workers_workerID",
                        column: x => x.workerID,
                        principalTable: "Workers",
                        principalColumn: "workerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Breakdowns",
                columns: table => new
                {
                    breakdownID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orderID = table.Column<int>(nullable: false),
                    partID = table.Column<int>(nullable: false),
                    workerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breakdowns", x => x.breakdownID);
                    table.ForeignKey(
                        name: "FK_Breakdowns_Orders_orderID",
                        column: x => x.orderID,
                        principalTable: "Orders",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Breakdowns_Parts_partID",
                        column: x => x.partID,
                        principalTable: "Parts",
                        principalColumn: "partID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Breakdowns_Workers_workerID",
                        column: x => x.workerID,
                        principalTable: "Workers",
                        principalColumn: "workerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breakdowns_orderID",
                table: "Breakdowns",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_Breakdowns_partID",
                table: "Breakdowns",
                column: "partID");

            migrationBuilder.CreateIndex(
                name: "IX_Breakdowns_workerID",
                table: "Breakdowns",
                column: "workerID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ownerID",
                table: "Cars",
                column: "ownerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_carID",
                table: "Orders",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_workerID",
                table: "Orders",
                column: "workerID");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_postID",
                table: "Workers",
                column: "postID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breakdowns");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
