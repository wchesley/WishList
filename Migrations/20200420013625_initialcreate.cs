using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WishList.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductMeta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductUrl = table.Column<string>(nullable: true),
                    PriceHtmlId = table.Column<string>(nullable: true),
                    NameHtmlId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMeta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    timeRetreived = table.Column<DateTime>(nullable: false),
                    price = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    productMetaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductMeta_productMetaId",
                        column: x => x.productMetaId,
                        principalTable: "ProductMeta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_productMetaId",
                table: "Product",
                column: "productMetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductMeta");
        }
    }
}
