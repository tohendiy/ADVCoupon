using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ADVCoupon.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Products_ProductId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Providers_ProviderId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Networks_NetworkBarcodes_NetworkBarcodeId",
                table: "Networks");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Networks_NetworkBarcodeId",
                table: "Networks");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_ProductId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_ProviderId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "NetworkBarcodeId",
                table: "Networks");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "Products",
                newName: "CouponId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                newName: "IX_Products_CouponId");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "NetworkBarcodes",
                nullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Networks",
                table: "NetworkBarcodes",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Coupons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NetworkBarcodes_CouponId",
                table: "NetworkBarcodes",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_NetworkBarcodes_Coupons_CouponId",
                table: "NetworkBarcodes",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Coupons_CouponId",
                table: "Products",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NetworkBarcodes_Coupons_CouponId",
                table: "NetworkBarcodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Coupons_CouponId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_NetworkBarcodes_CouponId",
                table: "NetworkBarcodes");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "NetworkBarcodes");

            migrationBuilder.DropColumn(
                name: "Networks",
                table: "NetworkBarcodes");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "CouponId",
                table: "Products",
                newName: "ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CouponId",
                table: "Products",
                newName: "IX_Products_ProductCategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "NetworkBarcodeId",
                table: "Networks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Coupons",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "Coupons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Networks_NetworkBarcodeId",
                table: "Networks",
                column: "NetworkBarcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductId",
                table: "Coupons",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProviderId",
                table: "Coupons",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Products_ProductId",
                table: "Coupons",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Providers_ProviderId",
                table: "Coupons",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Networks_NetworkBarcodes_NetworkBarcodeId",
                table: "Networks",
                column: "NetworkBarcodeId",
                principalTable: "NetworkBarcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
