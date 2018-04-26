using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ADVCoupon.Migrations
{
    public partial class NewChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Coupons_CouponId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CouponId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "NetworkId",
                table: "UserCoupon",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Coupons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductId",
                table: "Coupons",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Products_ProductId",
                table: "Coupons",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Products_ProductId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_ProductId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "UserCoupon");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Coupons");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CouponId",
                table: "Products",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Coupons_CouponId",
                table: "Products",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
