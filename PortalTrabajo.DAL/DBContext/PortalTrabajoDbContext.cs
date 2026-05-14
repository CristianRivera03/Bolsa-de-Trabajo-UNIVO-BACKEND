using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PortalTrabajo.Model;

public partial class PortalTrabajoDbContext : DbContext
{
    public PortalTrabajoDbContext()
    {
    }

    public PortalTrabajoDbContext(DbContextOptions<PortalTrabajoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlumnosActivo> AlumnosActivos { get; set; }

    public virtual DbSet<CatCarrera> CatCarreras { get; set; }

    public virtual DbSet<CatEstadosPostulacion> CatEstadosPostulacions { get; set; }

    public virtual DbSet<CatGradosAcademico> CatGradosAcademicos { get; set; }

    public virtual DbSet<CatModalidade> CatModalidades { get; set; }

    public virtual DbSet<CatNivelesIdioma> CatNivelesIdiomas { get; set; }

    public virtual DbSet<CatRole> CatRoles { get; set; }

    public virtual DbSet<Educacion> Educacions { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EstudianteHabilidade> EstudianteHabilidades { get; set; }

    public virtual DbSet<EstudianteIdioma> EstudianteIdiomas { get; set; }

    public virtual DbSet<ExperienciasLaborale> ExperienciasLaborales { get; set; }

    public virtual DbSet<Habilidade> Habilidades { get; set; }

    public virtual DbSet<OfertaHabilidade> OfertaHabilidades { get; set; }

    public virtual DbSet<OfertasLaborale> OfertasLaborales { get; set; }

    public virtual DbSet<PerfilesEstudiante> PerfilesEstudiantes { get; set; }

    public virtual DbSet<ProyectosEstudiante> ProyectosEstudiantes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlumnosActivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlumnosA__3214EC079F28C4A9");

            entity.ToTable("AlumnosActivos", "PortalNotasMock");

            entity.HasIndex(e => e.Carnet, "UQ__AlumnosA__5E387B4D6173647D").IsUnique();

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Carnet).HasMaxLength(20);
            entity.Property(e => e.Carrera).HasMaxLength(150);
            entity.Property(e => e.EstadoAcademico)
                .HasMaxLength(50)
                .HasDefaultValue("Activo");
            entity.Property(e => e.Facultad).HasMaxLength(150);
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.PasswordPortal).HasMaxLength(256);
        });

        modelBuilder.Entity<CatCarrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CatCarre__3214EC0719BD5C92");

            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.Facultad).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<CatEstadosPostulacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CatEstad__3214EC07D59106FC");

            entity.ToTable("CatEstadosPostulacion");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<CatGradosAcademico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CatGrado__3214EC0723A16B8D");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<CatModalidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CatModal__3214EC071ECA6E8C");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<CatNivelesIdioma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CatNivel__3214EC07303040D3");

            entity.ToTable("CatNivelesIdioma");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<CatRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CatRoles__3214EC072D9429AC");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Educacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Educacio__3214EC0726CD9B68");

            entity.ToTable("Educacion");

            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.Institucion).HasMaxLength(200);
            entity.Property(e => e.TituloObtenido).HasMaxLength(200);

            entity.HasOne(d => d.GradoAcademico).WithMany(p => p.Educacions)
                .HasForeignKey(d => d.GradoAcademicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Educacion_Grado");

            entity.HasOne(d => d.Perfil).WithMany(p => p.Educacions)
                .HasForeignKey(d => d.PerfilId)
                .HasConstraintName("FK_Educacion_Perfil");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empresas__3214EC072876EC97");

            entity.HasIndex(e => e.UsuarioId, "UQ__Empresas__2B3DE7B99A6E4E70").IsUnique();

            entity.Property(e => e.LogoUrl).HasMaxLength(2048);
            entity.Property(e => e.NombreComercial).HasMaxLength(200);
            entity.Property(e => e.Sector).HasMaxLength(100);
            entity.Property(e => e.SitioWeb).HasMaxLength(2048);

            entity.HasOne(d => d.Usuario).WithOne(p => p.Empresa)
                .HasForeignKey<Empresa>(d => d.UsuarioId)
                .HasConstraintName("FK_Empresas_Usuario");
        });

        modelBuilder.Entity<EstudianteHabilidade>(entity =>
        {
            entity.HasKey(e => new { e.PerfilId, e.HabilidadId }).HasName("PK__Estudian__3B3444E48BFCED8D");

            entity.HasOne(d => d.Habilidad).WithMany(p => p.EstudianteHabilidades)
                .HasForeignKey(d => d.HabilidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hab_Habilidad");

            entity.HasOne(d => d.Perfil).WithMany(p => p.EstudianteHabilidades)
                .HasForeignKey(d => d.PerfilId)
                .HasConstraintName("FK_Hab_Perfil");
        });

        modelBuilder.Entity<EstudianteIdioma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estudian__3214EC07ACD1740A");

            entity.Property(e => e.Idioma).HasMaxLength(100);

            entity.HasOne(d => d.Nivel).WithMany(p => p.EstudianteIdiomas)
                .HasForeignKey(d => d.NivelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Idiomas_Nivel");

            entity.HasOne(d => d.Perfil).WithMany(p => p.EstudianteIdiomas)
                .HasForeignKey(d => d.PerfilId)
                .HasConstraintName("FK_Idiomas_Perfil");
        });

        modelBuilder.Entity<ExperienciasLaborale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Experien__3214EC0708A94314");

            entity.Property(e => e.Cargo).HasMaxLength(150);
            entity.Property(e => e.Empresa).HasMaxLength(200);
            entity.Property(e => e.EsTrabajoActual).HasDefaultValue(false);

            entity.HasOne(d => d.Perfil).WithMany(p => p.ExperienciasLaborales)
                .HasForeignKey(d => d.PerfilId)
                .HasConstraintName("FK_ExpLab_Perfil");
        });

        modelBuilder.Entity<Habilidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habilida__3214EC0799335888");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<OfertaHabilidade>(entity =>
        {
            entity.HasKey(e => new { e.OfertaId, e.HabilidadId }).HasName("PK__OfertaHa__C5568BCB41F49F68");

            entity.Property(e => e.EsObligatorio).HasDefaultValue(true);

            entity.HasOne(d => d.Habilidad).WithMany(p => p.OfertaHabilidades)
                .HasForeignKey(d => d.HabilidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfertaHab_Habilidad");

            entity.HasOne(d => d.Oferta).WithMany(p => p.OfertaHabilidades)
                .HasForeignKey(d => d.OfertaId)
                .HasConstraintName("FK_OfertaHab_Oferta");
        });

        modelBuilder.Entity<OfertasLaborale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OfertasL__3214EC073DAF7E5D");

            entity.HasIndex(e => new { e.Activa, e.FechaExpiracion }, "IX_Ofertas_Activa");

            entity.HasIndex(e => e.EmpresaId, "IX_Ofertas_EmpresaId");

            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.FechaPublicacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SalarioMax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SalarioMin).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Titulo).HasMaxLength(200);
            entity.Property(e => e.Ubicacion).HasMaxLength(200);

            entity.HasOne(d => d.Empresa).WithMany(p => p.OfertasLaborales)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK_Ofertas_Empresa");

            entity.HasOne(d => d.Modalidad).WithMany(p => p.OfertasLaborales)
                .HasForeignKey(d => d.ModalidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ofertas_Modalidad");

            entity.HasMany(d => d.Carreras).WithMany(p => p.Oferta)
                .UsingEntity<Dictionary<string, object>>(
                    "OfertaCarrera",
                    r => r.HasOne<CatCarrera>().WithMany()
                        .HasForeignKey("CarreraId")
                        .HasConstraintName("FK_OfertaCarreras_Carrera"),
                    l => l.HasOne<OfertasLaborale>().WithMany()
                        .HasForeignKey("OfertaId")
                        .HasConstraintName("FK_OfertaCarreras_Oferta"),
                    j =>
                    {
                        j.HasKey("OfertaId", "CarreraId").HasName("PK__OfertaCa__F186AF337FFF2F6D");
                        j.ToTable("OfertaCarreras");
                    });
        });

        modelBuilder.Entity<PerfilesEstudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Perfiles__3214EC0725DCF04E");

            entity.HasIndex(e => e.UsuarioId, "UQ__Perfiles__2B3DE7B9779DB5CD").IsUnique();

            entity.HasIndex(e => e.Carnet, "UQ__Perfiles__5E387B4D0887FBB9").IsUnique();

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.BuscaEmpleo).HasDefaultValue(true);
            entity.Property(e => e.Carnet).HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.EnlaceGitHub).HasMaxLength(2048);
            entity.Property(e => e.EnlaceLinkedIn).HasMaxLength(2048);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FotoUrl).HasMaxLength(2048);
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.Carrera).WithMany(p => p.PerfilesEstudiantes)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK_PerfilesEstudiantes_Carrera");

            entity.HasOne(d => d.Usuario).WithOne(p => p.PerfilesEstudiante)
                .HasForeignKey<PerfilesEstudiante>(d => d.UsuarioId)
                .HasConstraintName("FK_PerfilesEstudiantes_Usuario");
        });

        modelBuilder.Entity<ProyectosEstudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07E99BA583");

            entity.ToTable("ProyectosEstudiante");

            entity.Property(e => e.EnlaceRepositorio).HasMaxLength(2048);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.TecnologiasUsadas).HasMaxLength(500);

            entity.HasOne(d => d.Perfil).WithMany(p => p.ProyectosEstudiantes)
                .HasForeignKey(d => d.PerfilId)
                .HasConstraintName("FK_Proyectos_Perfil");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07F7DA8A0D");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534FB1CB407").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PasswordHash).HasMaxLength(256);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
