using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ADVCoupon.Migrations
{
    public partial class NetworkBarcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Networks",
                table: "NetworkBarcodes");

            migrationBuilder.AddColumn<Guid>(
                name: "NetworkBarcodeId",
                table: "Networks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Networks_NetworkBarcodeId",
                table: "Networks",
                column: "NetworkBarcodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Networks_NetworkBarcodes_NetworkBarcodeId",
                table: "Networks",
                column: "NetworkBarcodeId",
                principalTable: "NetworkBarcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Networks_NetworkBarcodes_NetworkBarcodeId",
                table: "Networks");

            migrationBuilder.DropIndex(
                name: "IX_Networks_NetworkBarcodeId",
                table: "Networks");

            migrationBuilder.DropColumn(
                name: "NetworkBarcodeId",
                table: "Networks");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Networks",
                table: "NetworkBarcodes",
                nullable: true);
        }
    }
}
