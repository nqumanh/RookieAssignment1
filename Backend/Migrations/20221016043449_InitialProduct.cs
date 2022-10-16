using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class InitialProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Products_ProductId",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Customers_CustomerId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Products_ProductId",
                table: "Rating");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Rating",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "Rating",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "OrderLine",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "OrderId",
                table: "OrderLine",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "Order",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                table: "OrderLine",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Products_ProductId",
                table: "OrderLine",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Customers_CustomerId",
                table: "Rating",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Products_ProductId",
                table: "Rating",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Products_ProductId",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Customers_CustomerId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Products_ProductId",
                table: "Rating");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Rating",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "Rating",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "OrderLine",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OrderId",
                table: "OrderLine",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "Order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                table: "OrderLine",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Products_ProductId",
                table: "OrderLine",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Customers_CustomerId",
                table: "Rating",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Products_ProductId",
                table: "Rating",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
