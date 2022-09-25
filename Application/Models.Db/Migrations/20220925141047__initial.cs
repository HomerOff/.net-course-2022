using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Db.Migrations
{
    public partial class _initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    passport = table.Column<int>(type: "integer", nullable: false),
                    birthday_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    phone_number = table.Column<int>(type: "integer", nullable: false),
                    bonus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    passport = table.Column<int>(type: "integer", nullable: false),
                    birthday_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    phone_number = table.Column<int>(type: "integer", nullable: false),
                    bonus = table.Column<int>(type: "integer", nullable: false),
                    salary = table.Column<int>(type: "integer", nullable: false),
                    contract = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_accounts_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountCurrency",
                columns: table => new
                {
                    AccountsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrenciesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCurrency", x => new { x.AccountsId, x.CurrenciesId });
                    table.ForeignKey(
                        name: "FK_AccountCurrency_accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCurrency_currencies_CurrenciesId",
                        column: x => x.CurrenciesId,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountCurrency_CurrenciesId",
                table: "AccountCurrency",
                column: "CurrenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_ClientId",
                table: "accounts",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCurrency");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
