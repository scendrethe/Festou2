using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Festou2.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteIdade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteCPF = table.Column<int>(type: "int", nullable: false),
                    ClienteBudget = table.Column<decimal>(type: "decimal(38,10)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Locadores",
                columns: table => new
                {
                    LocadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocadorNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocadorDescricaoAmbiente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ambiente = table.Column<int>(type: "int", nullable: false),
                    LocadorIdade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocadorCPF = table.Column<int>(type: "int", nullable: false),
                    LocadorPreco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locadores", x => x.LocadorId);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    LocalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ambiente = table.Column<int>(type: "int", nullable: false),
                    QtdPessoas = table.Column<int>(type: "int", nullable: false),
                    tipoFesta = table.Column<int>(type: "int", nullable: false),
                    LocadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.LocalId);
                    table.ForeignKey(
                        name: "FK_Local_Locadores_LocadorId",
                        column: x => x.LocadorId,
                        principalTable: "Locadores",
                        principalColumn: "LocadorId");
                });

            migrationBuilder.CreateTable(
                name: "LinkDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    LocadorId = table.Column<int>(type: "int", nullable: true),
                    LocalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkDto_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_LinkDto_Locadores_LocadorId",
                        column: x => x.LocadorId,
                        principalTable: "Locadores",
                        principalColumn: "LocadorId");
                    table.ForeignKey(
                        name: "FK_LinkDto_Local_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Local",
                        principalColumn: "LocalId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkDto_ClienteId",
                table: "LinkDto",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkDto_LocadorId",
                table: "LinkDto",
                column: "LocadorId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkDto_LocalId",
                table: "LinkDto",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Local_LocadorId",
                table: "Local",
                column: "LocadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkDto");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Local");

            migrationBuilder.DropTable(
                name: "Locadores");
        }
    }
}
