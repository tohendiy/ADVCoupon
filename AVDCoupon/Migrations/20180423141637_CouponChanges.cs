using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ADVCoupon.Migrations
{
    public partial class CouponChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "NetworkBarcodes");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Coupons",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Coupons");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "NetworkBarcodes",
                nullable: false,
                defaultValue: false);
        }
    }
}
