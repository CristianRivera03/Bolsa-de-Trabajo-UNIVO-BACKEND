using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalTrabajo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCatSector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatCarreras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Facultad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatCarre__3214EC0719BD5C92", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatDepartamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatDepar__3214EC073E894DBC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatEstadosPostulacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatEstad__3214EC07D59106FC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatGeneros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatGener__3214EC071D80B2F5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatGradosAcademicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatGrado__3214EC0723A16B8D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatModalidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatModal__3214EC071ECA6E8C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatNivelesIdioma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatNivel__3214EC07303040D3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatRoles__3214EC072D9429AC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatSectores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatSectores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatTiposContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatTipos__3214EC0731A7648D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatTiposLicencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatTipos__3214EC07049DC62F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Habilida__3214EC0799335888", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatMunicipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatMunic__3214EC074298774A", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CatMunici__Depar__3E1D39E1",
                        column: x => x.DepartamentoId,
                        principalTable: "CatDepartamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__3214EC07F7DA8A0D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Rol",
                        column: x => x.RolId,
                        principalTable: "CatRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CatDistritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatDistr__3214EC07C01AA7A4", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distritos_Municipio",
                        column: x => x.MunicipioId,
                        principalTable: "CatMunicipios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SitioWeb = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NIT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorreoInstitucional = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empresas__3214EC072876EC97", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Sector",
                        column: x => x.SectorId,
                        principalTable: "CatSectores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Empresas_Usuario",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilesEstudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Carnet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genero = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SobreMi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    EnlaceGitHub = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    EnlaceLinkedIn = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarreraId = table.Column<int>(type: "int", nullable: true),
                    BuscaEmpleo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Perfiles__3214EC0725DCF04E", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_Carrera",
                        column: x => x.CarreraId,
                        principalTable: "CatCarreras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerfilesEstudiantes_Usuario",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactosEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DUI = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TelefonoMovil = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CorreoContacto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contacto__3214EC0768A6AAE4", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacto_Empresa",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfertasLaborales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requisitos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModalidadId = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SalarioMin = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SalarioMax = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Vacantes = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    EdadMin = table.Column<int>(type: "int", nullable: true),
                    EdadMax = table.Column<int>(type: "int", nullable: true),
                    TieneVehiculo = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    LicenciaId = table.Column<int>(type: "int", nullable: true),
                    TipoContratoId = table.Column<int>(type: "int", nullable: true),
                    GeneroId = table.Column<int>(type: "int", nullable: true),
                    DistritoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OfertasL__3214EC073DAF7E5D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertas_Contrato",
                        column: x => x.TipoContratoId,
                        principalTable: "CatTiposContrato",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ofertas_Distrito",
                        column: x => x.DistritoId,
                        principalTable: "CatDistritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ofertas_Empresa",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ofertas_Genero",
                        column: x => x.GeneroId,
                        principalTable: "CatGeneros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ofertas_Licencia",
                        column: x => x.LicenciaId,
                        principalTable: "CatTiposLicencia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ofertas_Modalidad",
                        column: x => x.ModalidadId,
                        principalTable: "CatModalidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Educacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    GradoAcademicoId = table.Column<int>(type: "int", nullable: false),
                    Institucion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TituloObtenido = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Educacio__3214EC0726CD9B68", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educacion_Grado",
                        column: x => x.GradoAcademicoId,
                        principalTable: "CatGradosAcademicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Educacion_Perfil",
                        column: x => x.PerfilId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstudianteHabilidades",
                columns: table => new
                {
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    HabilidadId = table.Column<int>(type: "int", nullable: false),
                    NivelDominio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estudian__3B3444E48BFCED8D", x => new { x.PerfilId, x.HabilidadId });
                    table.ForeignKey(
                        name: "FK_Hab_Habilidad",
                        column: x => x.HabilidadId,
                        principalTable: "Habilidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hab_Perfil",
                        column: x => x.PerfilId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstudianteIdiomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    Idioma = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NivelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estudian__3214EC07ACD1740A", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idiomas_Nivel",
                        column: x => x.NivelId,
                        principalTable: "CatNivelesIdioma",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Idiomas_Perfil",
                        column: x => x.PerfilId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciasLaborales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    Empresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: true),
                    EsTrabajoActual = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    DescripcionPuesto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Experien__3214EC0708A94314", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpLab_Perfil",
                        column: x => x.PerfilId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyectosEstudiante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TecnologiasUsadas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EnlaceRepositorio = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    FechaProyecto = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proyecto__3214EC07E99BA583", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyectos_Perfil",
                        column: x => x.PerfilId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfertaCarreras",
                columns: table => new
                {
                    OfertaId = table.Column<int>(type: "int", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OfertaCa__F186AF337FFF2F6D", x => new { x.OfertaId, x.CarreraId });
                    table.ForeignKey(
                        name: "FK_OfertaCarreras_Carrera",
                        column: x => x.CarreraId,
                        principalTable: "CatCarreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertaCarreras_Oferta",
                        column: x => x.OfertaId,
                        principalTable: "OfertasLaborales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfertaHabilidades",
                columns: table => new
                {
                    OfertaId = table.Column<int>(type: "int", nullable: false),
                    HabilidadId = table.Column<int>(type: "int", nullable: false),
                    EsObligatorio = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OfertaHa__C5568BCB41F49F68", x => new { x.OfertaId, x.HabilidadId });
                    table.ForeignKey(
                        name: "FK_OfertaHab_Habilidad",
                        column: x => x.HabilidadId,
                        principalTable: "Habilidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfertaHab_Oferta",
                        column: x => x.OfertaId,
                        principalTable: "OfertasLaborales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postulaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfertaId = table.Column<int>(type: "int", nullable: false),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    FechaPostulacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    EstadoId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Mensaje = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Postulac__3214EC079C1BB18B", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postulaciones_Estados",
                        column: x => x.EstadoId,
                        principalTable: "CatEstadosPostulacion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Postulaciones_Ofertas",
                        column: x => x.OfertaId,
                        principalTable: "OfertasLaborales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Postulaciones_Perfiles",
                        column: x => x.PerfilId,
                        principalTable: "PerfilesEstudiantes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__AlumnosA__5E387B4D6173647D",
                schema: "PortalNotasMock",
                table: "AlumnosActivos",
                column: "Carnet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatDistritos_MunicipioId",
                table: "CatDistritos",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_CatMunicipios_DepartamentoId",
                table: "CatMunicipios",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "UQ__Contacto__7B9F21175146502A",
                table: "ContactosEmpresa",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educacion_GradoAcademicoId",
                table: "Educacion",
                column: "GradoAcademicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Educacion_PerfilId",
                table: "Educacion",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_SectorId",
                table: "Empresas",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "UQ__Empresas__2B3DE7B99A6E4E70",
                table: "Empresas",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteHabilidades_HabilidadId",
                table: "EstudianteHabilidades",
                column: "HabilidadId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteIdiomas_NivelId",
                table: "EstudianteIdiomas",
                column: "NivelId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteIdiomas_PerfilId",
                table: "EstudianteIdiomas",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciasLaborales_PerfilId",
                table: "ExperienciasLaborales",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaCarreras_CarreraId",
                table: "OfertaCarreras",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaHabilidades_HabilidadId",
                table: "OfertaHabilidades",
                column: "HabilidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_Activa",
                table: "OfertasLaborales",
                columns: new[] { "Activa", "FechaExpiracion" });

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_EmpresaId",
                table: "OfertasLaborales",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_DistritoId",
                table: "OfertasLaborales",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_GeneroId",
                table: "OfertasLaborales",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_LicenciaId",
                table: "OfertasLaborales",
                column: "LicenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_ModalidadId",
                table: "OfertasLaborales",
                column: "ModalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_TipoContratoId",
                table: "OfertasLaborales",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilesEstudiantes_CarreraId",
                table: "PerfilesEstudiantes",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "UQ__Perfiles__2B3DE7B9779DB5CD",
                table: "PerfilesEstudiantes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Perfiles__5E387B4D0887FBB9",
                table: "PerfilesEstudiantes",
                column: "Carnet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_EstadoId",
                table: "Postulaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_PerfilId",
                table: "Postulaciones",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "UQ_Postulacion_Oferta_Perfil",
                table: "Postulaciones",
                columns: new[] { "OfertaId", "PerfilId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosEstudiante_PerfilId",
                table: "ProyectosEstudiante",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__A9D10534FB1CB407",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "ContactosEmpresa");

            migrationBuilder.DropTable(
                name: "Educacion");

            migrationBuilder.DropTable(
                name: "EstudianteHabilidades");

            migrationBuilder.DropTable(
                name: "EstudianteIdiomas");

            migrationBuilder.DropTable(
                name: "ExperienciasLaborales");

            migrationBuilder.DropTable(
                name: "OfertaCarreras");

            migrationBuilder.DropTable(
                name: "OfertaHabilidades");

            migrationBuilder.DropTable(
                name: "Postulaciones");

            migrationBuilder.DropTable(
                name: "ProyectosEstudiante");

            migrationBuilder.DropTable(
                name: "CatGradosAcademicos");

            migrationBuilder.DropTable(
                name: "CatNivelesIdioma");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "CatEstadosPostulacion");

            migrationBuilder.DropTable(
                name: "OfertasLaborales");

            migrationBuilder.DropTable(
                name: "PerfilesEstudiantes");

            migrationBuilder.DropTable(
                name: "CatTiposContrato");

            migrationBuilder.DropTable(
                name: "CatDistritos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "CatGeneros");

            migrationBuilder.DropTable(
                name: "CatTiposLicencia");

            migrationBuilder.DropTable(
                name: "CatModalidades");

            migrationBuilder.DropTable(
                name: "CatCarreras");

            migrationBuilder.DropTable(
                name: "CatMunicipios");

            migrationBuilder.DropTable(
                name: "CatSectores");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CatDepartamentos");

            migrationBuilder.DropTable(
                name: "CatRoles");
        }
    }
}

