using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHorariosLaborales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioLaboral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    State = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioLaboral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoHorarioLaboral",
                columns: table => new
                {
                    EmpleadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    HorarioLaboralId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoHorarioLaboral", x => new { x.EmpleadoId, x.HorarioLaboralId });
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarioLaboral_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarioLaboral_HorarioLaboral_HorarioLaboralId",
                        column: x => x.HorarioLaboralId,
                        principalTable: "HorarioLaboral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorarioLaboralDetalle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Dia = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    HorarioLaboralId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioLaboralDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorarioLaboralDetalle_HorarioLaboral_HorarioLaboralId",
                        column: x => x.HorarioLaboralId,
                        principalTable: "HorarioLaboral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarioLaboral_HorarioLaboralId",
                table: "EmpleadoHorarioLaboral",
                column: "HorarioLaboralId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioLaboralDetalle_HorarioLaboralId",
                table: "HorarioLaboralDetalle",
                column: "HorarioLaboralId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadoHorarioLaboral");

            migrationBuilder.DropTable(
                name: "HorarioLaboralDetalle");

            migrationBuilder.DropTable(
                name: "HorarioLaboral");
        }
    }
}
