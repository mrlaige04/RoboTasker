﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nerbotix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TenantAndSuperUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tenants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tenants");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
