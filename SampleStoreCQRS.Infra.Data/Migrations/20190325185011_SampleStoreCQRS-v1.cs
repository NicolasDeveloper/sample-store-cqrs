using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleStoreCQRS.Infra.Data.Migrations
{
    public partial class SampleStoreCQRSv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Cod = table.Column<string>(nullable: true),
                    Percentage = table.Column<decimal>(nullable: false),
                    ValidadePeriodStart = table.Column<DateTime>(name: "ValidadePeriod.Start", nullable: false),
                    ValidadePeriodEnd = table.Column<DateTime>(name: "ValidadePeriod.End", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NameFirstName = table.Column<string>(name: "Name.FirstName", nullable: true),
                    NameLastName = table.Column<string>(name: "Name.LastName", nullable: true),
                    EmailAddress = table.Column<string>(name: "Email.Address", nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    DocumentNumber = table.Column<string>(name: "Document.Number", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreditCardNumber = table.Column<string>(name: "CreditCard.Number", type: "varchar(19)", maxLength: 19, nullable: true),
                    CreditCardCvv = table.Column<int>(name: "CreditCard.Cvv", nullable: false),
                    CreditCardValidate = table.Column<string>(name: "CreditCard.Validate", type: "varchar(19)", maxLength: 19, nullable: true),
                    CreditCardPrintName = table.Column<string>(name: "CreditCard.PrintName", type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    QuantityOnHand = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: true),
                    DiscountCuponCod = table.Column<string>(name: "DiscountCupon.Cod", nullable: true),
                    DiscountCuponPercentage = table.Column<decimal>(name: "DiscountCupon.Percentage", nullable: false),
                    DiscountCuponValidadePeriodStart = table.Column<DateTime>(name: "DiscountCupon.ValidadePeriod.Start", nullable: false),
                    DiscountCuponValidadePeriodEnd = table.Column<DateTime>(name: "DiscountCupon.ValidadePeriod.End", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_Cod",
                table: "Cupons",
                column: "Cod",
                unique: true,
                filter: "[Cod] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Document.Number",
                table: "Customers",
                column: "Document.Number",
                unique: true,
                filter: "[Document.Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email.Address",
                table: "Customers",
                column: "Email.Address",
                unique: true,
                filter: "[Email.Address] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Number",
                table: "Orders",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupons");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
