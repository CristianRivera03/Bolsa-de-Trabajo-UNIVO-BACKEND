using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalTrabajo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditAndSoftDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ofertas_Activa",
                table: "OfertasLaborales");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "OfertasLaborales");

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "PerfilesEstudiantes",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "OfertasLaborales",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "Activa",
                table: "CatCarreras",
                newName: "Activo");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ProyectosEstudiante",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ProyectosEstudiante",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ProyectosEstudiante",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "ProyectosEstudiante",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Postulaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Postulaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Postulaciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "Postulaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "PerfilesEstudiantes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "PerfilesEstudiantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "PerfilesEstudiantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "OfertasLaborales",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "OfertasLaborales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "OfertasLaborales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "OfertaHabilidades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "OfertaHabilidades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "OfertaHabilidades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "OfertaHabilidades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Habilidades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Habilidades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Habilidades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "Habilidades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ExperienciasLaborales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ExperienciasLaborales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ExperienciasLaborales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "ExperienciasLaborales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "EstudianteIdiomas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "EstudianteIdiomas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "EstudianteIdiomas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "EstudianteIdiomas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "EstudianteHabilidades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "EstudianteHabilidades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "EstudianteHabilidades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "EstudianteHabilidades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Empresas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Empresas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Empresas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "Empresas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Educacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Educacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Educacion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "Educacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ContactosEmpresa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ContactosEmpresa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ContactosEmpresa",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "ContactosEmpresa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatTiposLicencia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatTiposLicencia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatTiposLicencia",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatTiposLicencia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatTiposContrato",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatTiposContrato",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatTiposContrato",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatTiposContrato",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatSectores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatSectores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatSectores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatSectores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatNivelesIdioma",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatNivelesIdioma",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatNivelesIdioma",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatNivelesIdioma",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatMunicipios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatMunicipios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatMunicipios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatMunicipios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatModalidades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatModalidades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatModalidades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatModalidades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatGradosAcademicos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatGradosAcademicos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatGradosAcademicos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatGradosAcademicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatGeneros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatGeneros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatGeneros",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatGeneros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatEstadosPostulacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatEstadosPostulacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatEstadosPostulacion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatEstadosPostulacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatDistritos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatDistritos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatDistritos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatDistritos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CatDepartamentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatDepartamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatDepartamentos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatDepartamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CatCarreras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CatCarreras",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacionId",
                table: "CatCarreras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTabla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValoresAntiguos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValoresNuevos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_Activa",
                table: "OfertasLaborales",
                columns: new[] { "Activo", "FechaExpiracion" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_Ofertas_Activa",
                table: "OfertasLaborales");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ProyectosEstudiante");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ProyectosEstudiante");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ProyectosEstudiante");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "ProyectosEstudiante");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Postulaciones");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Postulaciones");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Postulaciones");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "Postulaciones");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "PerfilesEstudiantes");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "PerfilesEstudiantes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "OfertasLaborales");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "OfertasLaborales");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "OfertasLaborales");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "OfertaHabilidades");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "OfertaHabilidades");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "OfertaHabilidades");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "OfertaHabilidades");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Habilidades");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Habilidades");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Habilidades");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "Habilidades");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ExperienciasLaborales");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ExperienciasLaborales");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ExperienciasLaborales");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "ExperienciasLaborales");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EstudianteIdiomas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "EstudianteIdiomas");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "EstudianteIdiomas");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "EstudianteIdiomas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EstudianteHabilidades");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "EstudianteHabilidades");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "EstudianteHabilidades");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "EstudianteHabilidades");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Educacion");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Educacion");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Educacion");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "Educacion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ContactosEmpresa");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ContactosEmpresa");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ContactosEmpresa");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "ContactosEmpresa");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatTiposLicencia");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatTiposLicencia");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatTiposLicencia");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatTiposLicencia");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatTiposContrato");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatTiposContrato");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatTiposContrato");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatTiposContrato");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatSectores");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatSectores");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatSectores");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatSectores");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatRoles");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatRoles");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatRoles");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatRoles");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatNivelesIdioma");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatNivelesIdioma");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatNivelesIdioma");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatNivelesIdioma");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatMunicipios");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatMunicipios");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatMunicipios");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatMunicipios");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatModalidades");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatModalidades");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatModalidades");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatModalidades");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatGradosAcademicos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatGradosAcademicos");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatGradosAcademicos");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatGradosAcademicos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatGeneros");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatGeneros");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatGeneros");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatGeneros");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatEstadosPostulacion");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatEstadosPostulacion");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatEstadosPostulacion");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatEstadosPostulacion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatDistritos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatDistritos");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatDistritos");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatDistritos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CatDepartamentos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatDepartamentos");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatDepartamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatDepartamentos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CatCarreras");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CatCarreras");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "CatCarreras");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "PerfilesEstudiantes",
                newName: "FechaActualizacion");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "OfertasLaborales",
                newName: "FechaActualizacion");

            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "CatCarreras",
                newName: "Activa");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Usuarios",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "PerfilesEstudiantes",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "OfertasLaborales",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_Activa",
                table: "OfertasLaborales",
                columns: new[] { "Activa", "FechaExpiracion" });
        }
    }
}

