using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StraviaTECApi.Models
{
    public partial class StravaContext : DbContext
    {
        public StravaContext()
        {
        }

        public StravaContext(DbContextOptions<StravaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<AmigoDeportista> AmigoDeportista { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<CarreraCategoria> CarreraCategoria { get; set; }
        public virtual DbSet<CarreraCuentabancaria> CarreraCuentabancaria { get; set; }
        public virtual DbSet<CarreraPatrocinador> CarreraPatrocinador { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Deportista> Deportista { get; set; }
        public virtual DbSet<DeportistaCarrera> DeportistaCarrera { get; set; }
        public virtual DbSet<DeportistaReto> DeportistaReto { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<GrupoCarrera> GrupoCarrera { get; set; }
        public virtual DbSet<GrupoDeportista> GrupoDeportista { get; set; }
        public virtual DbSet<GrupoReto> GrupoReto { get; set; }
        public virtual DbSet<Inscripcion> Inscripcion { get; set; }
        public virtual DbSet<InscripcionCarrera> InscripcionCarrera { get; set; }
        public virtual DbSet<Patrocinador> Patrocinador { get; set; }
        public virtual DbSet<Reto> Reto { get; set; }
        public virtual DbSet<RetoPatrocinador> RetoPatrocinador { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost;Database=StraviaDB;User Id=StraviaTECAdmin;Password=password;Port=5432");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Fechahora })
                    .HasName("actividad_pkey");

                entity.ToTable("actividad");

                entity.HasIndex(e => e.Fechahora)
                    .HasName("actividad_fechahora_key")
                    .IsUnique();

                entity.HasIndex(e => e.Usuariodeportista)
                    .HasName("actividad_usuariodeportista_key")
                    .IsUnique();

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Fechahora).HasColumnName("fechahora");

                entity.Property(e => e.Duracion)
                    .IsRequired()
                    .HasColumnName("duracion")
                    .HasMaxLength(10);

                entity.Property(e => e.Kilometraje).HasColumnName("kilometraje");

                entity.Property(e => e.Recorridogpx).HasColumnName("recorridogpx");

                entity.Property(e => e.Tipoactividad)
                    .IsRequired()
                    .HasColumnName("tipoactividad")
                    .HasMaxLength(20);

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithOne(p => p.Actividad)
                    .HasForeignKey<Actividad>(d => d.Usuariodeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("actividad_usuariodeportista_fkey");
            });

            modelBuilder.Entity<AmigoDeportista>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Amigoid })
                    .HasName("amigo_deportista_pkey");

                entity.ToTable("amigo_deportista");

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Amigoid)
                    .HasColumnName("amigoid")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Amigo)
                    .WithMany(p => p.AmigoDeportistaAmigo)
                    .HasForeignKey(d => d.Amigoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("amigo_deportista_amigoid_fkey");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.AmigoDeportistaUsuariodeportistaNavigation)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("amigo_deportista_usuariodeportista_fkey");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Admindeportista })
                    .HasName("carrera_pkey");

                entity.ToTable("carrera");

                entity.HasIndex(e => e.Admindeportista)
                    .HasName("carrera_admindeportista_key")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre)
                    .HasName("carrera_nombre_key")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Privacidad).HasColumnName("privacidad");

                entity.Property(e => e.Recorrido)
                    .IsRequired()
                    .HasColumnName("recorrido");

                entity.Property(e => e.Tipoactividad)
                    .IsRequired()
                    .HasColumnName("tipoactividad")
                    .HasMaxLength(30);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithOne(p => p.Carrera)
                    .HasForeignKey<Carrera>(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_admindeportista_fkey");
            });

            modelBuilder.Entity<CarreraCategoria>(entity =>
            {
                entity.HasKey(e => new { e.Nombrecategoria, e.Nombrecarrera, e.Admindeportista })
                    .HasName("carrera_categoria_pkey");

                entity.ToTable("carrera_categoria");

                entity.Property(e => e.Nombrecategoria)
                    .HasColumnName("nombrecategoria")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.CarreraCategoriaAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_categoria_admindeportista_fkey");

                entity.HasOne(d => d.NombrecarreraNavigation)
                    .WithMany(p => p.CarreraCategoriaNombrecarreraNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrecarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_categoria_nombrecarrera_fkey");

                entity.HasOne(d => d.NombrecategoriaNavigation)
                    .WithMany(p => p.CarreraCategoria)
                    .HasForeignKey(d => d.Nombrecategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_categoria_nombrecategoria_fkey");
            });

            modelBuilder.Entity<CarreraCuentabancaria>(entity =>
            {
                entity.HasKey(e => new { e.Nombrecarrera, e.Admindeportista })
                    .HasName("carrera_cuentabancaria_pkey");

                entity.ToTable("carrera_cuentabancaria");

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Cuentabancaria)
                    .IsRequired()
                    .HasColumnName("cuentabancaria")
                    .HasMaxLength(50);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.CarreraCuentabancariaAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_cuentabancaria_admindeportista_fkey");

                entity.HasOne(d => d.NombrecarreraNavigation)
                    .WithMany(p => p.CarreraCuentabancariaNombrecarreraNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrecarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_cuentabancaria_nombrecarrera_fkey");
            });

            modelBuilder.Entity<CarreraPatrocinador>(entity =>
            {
                entity.HasKey(e => new { e.Nombrepatrocinador, e.Nombrecarrera, e.Admindeportista })
                    .HasName("carrera_patrocinador_pkey");

                entity.ToTable("carrera_patrocinador");

                entity.Property(e => e.Nombrepatrocinador)
                    .HasColumnName("nombrepatrocinador")
                    .HasMaxLength(30);

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.CarreraPatrocinadorAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_patrocinador_admindeportista_fkey");

                entity.HasOne(d => d.NombrecarreraNavigation)
                    .WithMany(p => p.CarreraPatrocinadorNombrecarreraNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrecarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_patrocinador_nombrecarrera_fkey");

                entity.HasOne(d => d.NombrepatrocinadorNavigation)
                    .WithMany(p => p.CarreraPatrocinador)
                    .HasForeignKey(d => d.Nombrepatrocinador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_patrocinador_nombrepatrocinador_fkey");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("categoria_pkey");

                entity.ToTable("categoria");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(20);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Deportista>(entity =>
            {
                entity.HasKey(e => e.Usuario)
                    .HasName("deportista_pkey");

                entity.ToTable("deportista");

                entity.Property(e => e.Usuario)
                    .HasColumnName("usuario")
                    .HasMaxLength(20);

                entity.Property(e => e.Apellido1)
                    .IsRequired()
                    .HasColumnName("apellido1")
                    .HasMaxLength(20);

                entity.Property(e => e.Apellido2)
                    .IsRequired()
                    .HasColumnName("apellido2")
                    .HasMaxLength(20);

                entity.Property(e => e.Claveacceso)
                    .IsRequired()
                    .HasColumnName("claveacceso")
                    .HasMaxLength(20);

                entity.Property(e => e.Fechanacimiento)
                    .HasColumnName("fechanacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Foto)
                    .IsRequired()
                    .HasColumnName("foto");

                entity.Property(e => e.Nacionalidad)
                    .IsRequired()
                    .HasColumnName("nacionalidad")
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombrecategoria)
                    .HasColumnName("nombrecategoria")
                    .HasMaxLength(20);

                entity.HasOne(d => d.NombrecategoriaNavigation)
                    .WithMany(p => p.Deportista)
                    .HasForeignKey(d => d.Nombrecategoria)
                    .HasConstraintName("deportista_nombrecategoria_fkey");
            });

            modelBuilder.Entity<DeportistaCarrera>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Nombrecarrera, e.Admindeportista })
                    .HasName("deportista_carrera_pkey");

                entity.ToTable("deportista_carrera");

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Completada).HasColumnName("completada");

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.DeportistaCarreraAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deportista_carrera_admindeportista_fkey");

                entity.HasOne(d => d.NombrecarreraNavigation)
                    .WithMany(p => p.DeportistaCarreraNombrecarreraNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrecarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deportista_carrera_nombrecarrera_fkey");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.DeportistaCarrera)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deportista_carrera_usuariodeportista_fkey");
            });

            modelBuilder.Entity<DeportistaReto>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Nombrereto, e.Admindeportista })
                    .HasName("deportista_reto_pkey");

                entity.ToTable("deportista_reto");

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombrereto)
                    .HasColumnName("nombrereto")
                    .HasMaxLength(20);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Completado).HasColumnName("completado");

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.DeportistaRetoAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deportista_reto_admindeportista_fkey");

                entity.HasOne(d => d.NombreretoNavigation)
                    .WithMany(p => p.DeportistaRetoNombreretoNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrereto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deportista_reto_nombrereto_fkey");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.DeportistaReto)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deportista_reto_usuariodeportista_fkey");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Admindeportista })
                    .HasName("grupo_pkey");

                entity.ToTable("grupo");

                entity.HasIndex(e => e.Admindeportista)
                    .HasName("grupo_admindeportista_key")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre)
                    .HasName("grupo_nombre_key")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithOne(p => p.Grupo)
                    .HasForeignKey<Grupo>(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_admindeportista_fkey");
            });

            modelBuilder.Entity<GrupoCarrera>(entity =>
            {
                entity.HasKey(e => new { e.Nombrecarrera, e.Nombregrupo, e.Admindeportista })
                    .HasName("grupo_carrera_pkey");

                entity.ToTable("grupo_carrera");

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Nombregrupo)
                    .HasColumnName("nombregrupo")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.GrupoCarreraAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_carrera_admindeportista_fkey");

                entity.HasOne(d => d.NombrecarreraNavigation)
                    .WithMany(p => p.GrupoCarreraNombrecarreraNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrecarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_carrera_nombrecarrera_fkey");

                entity.HasOne(d => d.NombregrupoNavigation)
                    .WithMany(p => p.GrupoCarrera)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombregrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_carrera_nombregrupo_fkey");
            });

            modelBuilder.Entity<GrupoDeportista>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Nombregrupo, e.Admindeportista })
                    .HasName("grupo_deportista_pkey");

                entity.ToTable("grupo_deportista");

                entity.HasIndex(e => e.Nombregrupo)
                    .HasName("grupo_deportista_nombregrupo_key")
                    .IsUnique();

                entity.HasIndex(e => e.Usuariodeportista)
                    .HasName("grupo_deportista_usuariodeportista_key")
                    .IsUnique();

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombregrupo)
                    .HasColumnName("nombregrupo")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.GrupoDeportistaAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_deportista_admindeportista_fkey");

                entity.HasOne(d => d.NombregrupoNavigation)
                    .WithOne(p => p.GrupoDeportistaNombregrupoNavigation)
                    .HasPrincipalKey<Grupo>(p => p.Nombre)
                    .HasForeignKey<GrupoDeportista>(d => d.Nombregrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_deportista_nombregrupo_fkey");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithOne(p => p.GrupoDeportista)
                    .HasForeignKey<GrupoDeportista>(d => d.Usuariodeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_deportista_usuariodeportista_fkey");
            });

            modelBuilder.Entity<GrupoReto>(entity =>
            {
                entity.HasKey(e => new { e.Nombrereto, e.Nombregrupo, e.Admindeportista })
                    .HasName("grupo_reto_pkey");

                entity.ToTable("grupo_reto");

                entity.Property(e => e.Nombrereto)
                    .HasColumnName("nombrereto")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombregrupo)
                    .HasColumnName("nombregrupo")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.GrupoRetoAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_reto_admindeportista_fkey");

                entity.HasOne(d => d.NombregrupoNavigation)
                    .WithMany(p => p.GrupoReto)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombregrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_reto_nombregrupo_fkey");

                entity.HasOne(d => d.NombreretoNavigation)
                    .WithMany(p => p.GrupoRetoNombreretoNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrereto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("grupo_reto_nombrereto_fkey");
            });

            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Usuariodeportista })
                    .HasName("inscripcion_pkey");

                entity.ToTable("inscripcion");

                entity.HasIndex(e => e.Id)
                    .HasName("inscripcion_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.Usuariodeportista)
                    .HasName("inscripcion_usuariodeportista_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(10);

                entity.Property(e => e.Recibopago)
                    .IsRequired()
                    .HasColumnName("recibopago");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithOne(p => p.Inscripcion)
                    .HasForeignKey<Inscripcion>(d => d.Usuariodeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inscripcion_usuariodeportista_fkey");
            });

            modelBuilder.Entity<InscripcionCarrera>(entity =>
            {
                entity.HasKey(e => new { e.Idinscripcion, e.Nombrecarrera, e.Deportistainscripcion })
                    .HasName("inscripcion_carrera_pkey");

                entity.ToTable("inscripcion_carrera");

                entity.Property(e => e.Idinscripcion).HasColumnName("idinscripcion");

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Deportistainscripcion)
                    .HasColumnName("deportistainscripcion")
                    .HasMaxLength(20);

                entity.HasOne(d => d.DeportistainscripcionNavigation)
                    .WithMany(p => p.InscripcionCarreraDeportistainscripcionNavigation)
                    .HasPrincipalKey(p => p.Usuariodeportista)
                    .HasForeignKey(d => d.Deportistainscripcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inscripcion_carrera_deportistainscripcion_fkey");

                entity.HasOne(d => d.IdinscripcionNavigation)
                    .WithMany(p => p.InscripcionCarreraIdinscripcionNavigation)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.Idinscripcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inscripcion_carrera_idinscripcion_fkey");

                entity.HasOne(d => d.NombrecarreraNavigation)
                    .WithMany(p => p.InscripcionCarrera)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrecarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inscripcion_carrera_nombrecarrera_fkey");
            });

            modelBuilder.Entity<Patrocinador>(entity =>
            {
                entity.HasKey(e => e.Nombrecomercial)
                    .HasName("patrocinador_pkey");

                entity.ToTable("patrocinador");

                entity.Property(e => e.Nombrecomercial)
                    .HasColumnName("nombrecomercial")
                    .HasMaxLength(30);

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.Nombrerepresentante)
                    .IsRequired()
                    .HasColumnName("nombrerepresentante")
                    .HasMaxLength(100);

                entity.Property(e => e.Numerotelrepresentante)
                    .IsRequired()
                    .HasColumnName("numerotelrepresentante")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Reto>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Admindeportista })
                    .HasName("reto_pkey");

                entity.ToTable("reto");

                entity.HasIndex(e => e.Admindeportista)
                    .HasName("reto_admindeportista_key")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre)
                    .HasName("reto_nombre_key")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(20);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(150);

                entity.Property(e => e.Fondoaltitud)
                    .IsRequired()
                    .HasColumnName("fondoaltitud")
                    .HasMaxLength(7);

                entity.Property(e => e.Kmacumulados).HasColumnName("kmacumulados");

                entity.Property(e => e.Kmtotales).HasColumnName("kmtotales");

                entity.Property(e => e.Periododisponibilidad)
                    .HasColumnName("periododisponibilidad")
                    .HasColumnType("date");

                entity.Property(e => e.Privacidad).HasColumnName("privacidad");

                entity.Property(e => e.Tipoactividad)
                    .IsRequired()
                    .HasColumnName("tipoactividad")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithOne(p => p.Reto)
                    .HasForeignKey<Reto>(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reto_admindeportista_fkey");
            });

            modelBuilder.Entity<RetoPatrocinador>(entity =>
            {
                entity.HasKey(e => new { e.Nombrepatrocinador, e.Nombrereto, e.Admindeportista })
                    .HasName("reto_patrocinador_pkey");

                entity.ToTable("reto_patrocinador");

                entity.Property(e => e.Nombrepatrocinador)
                    .HasColumnName("nombrepatrocinador")
                    .HasMaxLength(30);

                entity.Property(e => e.Nombrereto)
                    .HasColumnName("nombrereto")
                    .HasMaxLength(20);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.RetoPatrocinadorAdmindeportistaNavigation)
                    .HasPrincipalKey(p => p.Admindeportista)
                    .HasForeignKey(d => d.Admindeportista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reto_patrocinador_admindeportista_fkey");

                entity.HasOne(d => d.NombrepatrocinadorNavigation)
                    .WithMany(p => p.RetoPatrocinador)
                    .HasForeignKey(d => d.Nombrepatrocinador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reto_patrocinador_nombrepatrocinador_fkey");

                entity.HasOne(d => d.NombreretoNavigation)
                    .WithMany(p => p.RetoPatrocinadorNombreretoNavigation)
                    .HasPrincipalKey(p => p.Nombre)
                    .HasForeignKey(d => d.Nombrereto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reto_patrocinador_nombrereto_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
