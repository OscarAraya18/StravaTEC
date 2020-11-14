using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StraviaTECApi.Models
{
    public partial class StraviaContext : DbContext
    {
        public StraviaContext()
        {
        }

        public StraviaContext(DbContextOptions<StraviaContext> options)
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
        public virtual DbSet<Patrocinador> Patrocinador { get; set; }
        public virtual DbSet<Reto> Reto { get; set; }
        public virtual DbSet<RetoPatrocinador> RetoPatrocinador { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost;Database=StraviaDB;User Id=StraviaTECAdmin;Password=password;Port=5432");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Fechahora })
                    .HasName("actividad_pkey");

                entity.ToTable("actividad");

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Fechahora).HasColumnName("fechahora");

                entity.Property(e => e.Adminretocarrera)
                    .HasColumnName("adminretocarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Banderilla).HasColumnName("banderilla");

                entity.Property(e => e.Duracion)
                    .HasColumnName("duracion")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Kilometraje).HasColumnName("kilometraje");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100);

                entity.Property(e => e.Nombreretocarrera)
                    .HasColumnName("nombreretocarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Recorridogpx)
                    .HasColumnName("recorridogpx")
                    .HasColumnType("xml");

                entity.Property(e => e.Tipoactividad)
                    .HasColumnName("tipoactividad")
                    .HasMaxLength(20);

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.Usuariodeportista)
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
                    .HasConstraintName("amigo_deportista_amigoid_fkey");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.AmigoDeportistaUsuariodeportistaNavigation)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .HasConstraintName("amigo_deportista_usuariodeportista_fkey");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Admindeportista })
                    .HasName("carrera_pkey");

                entity.ToTable("carrera");

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
                    .HasColumnName("recorrido")
                    .HasColumnType("xml");

                entity.Property(e => e.Tipoactividad)
                    .HasColumnName("tipoactividad")
                    .HasMaxLength(30);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.Carrera)
                    .HasForeignKey(d => d.Admindeportista)
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

                entity.HasOne(d => d.NombrecategoriaNavigation)
                    .WithMany(p => p.CarreraCategoria)
                    .HasForeignKey(d => d.Nombrecategoria)
                    .HasConstraintName("carrera_categoria_nombrecategoria_fkey");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.CarreraCategoria)
                    .HasForeignKey(d => new { d.Nombrecarrera, d.Admindeportista })
                    .HasConstraintName("carrera_categoria_nombrecarrera_admindeportista_fkey");
            });

            modelBuilder.Entity<CarreraCuentabancaria>(entity =>
            {
                entity.HasKey(e => new { e.Nombrecarrera, e.Admindeportista, e.Cuentabancaria })
                    .HasName("carrera_cuentabancaria_pkey");

                entity.ToTable("carrera_cuentabancaria");

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Cuentabancaria)
                    .HasColumnName("cuentabancaria")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.CarreraCuentabancaria)
                    .HasForeignKey(d => new { d.Nombrecarrera, d.Admindeportista })
                    .HasConstraintName("carrera_cuentabancaria_nombrecarrera_admindeportista_fkey");
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

                entity.HasOne(d => d.NombrepatrocinadorNavigation)
                    .WithMany(p => p.CarreraPatrocinador)
                    .HasForeignKey(d => d.Nombrepatrocinador)
                    .HasConstraintName("carrera_patrocinador_nombrepatrocinador_fkey");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.CarreraPatrocinador)
                    .HasForeignKey(d => new { d.Nombrecarrera, d.Admindeportista })
                    .HasConstraintName("carrera_patrocinador_nombrecarrera_admindeportista_fkey");
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
                    .HasColumnName("apellido1")
                    .HasMaxLength(20);

                entity.Property(e => e.Apellido2)
                    .HasColumnName("apellido2")
                    .HasMaxLength(20);

                entity.Property(e => e.Claveacceso)
                    .IsRequired()
                    .HasColumnName("claveacceso")
                    .HasMaxLength(20);

                entity.Property(e => e.Fechanacimiento)
                    .HasColumnName("fechanacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Foto).HasColumnName("foto");

                entity.Property(e => e.Nacionalidad)
                    .HasColumnName("nacionalidad")
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombrecategoria)
                    .HasColumnName("nombrecategoria")
                    .HasMaxLength(20);

                entity.HasOne(d => d.NombrecategoriaNavigation)
                    .WithMany(p => p.Deportista)
                    .HasForeignKey(d => d.Nombrecategoria)
                    .OnDelete(DeleteBehavior.Cascade)
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

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.DeportistaCarrera)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .HasConstraintName("deportista_carrera_usuariodeportista_fkey");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.DeportistaCarrera)
                    .HasForeignKey(d => new { d.Nombrecarrera, d.Admindeportista })
                    .HasConstraintName("deportista_carrera_nombrecarrera_admindeportista_fkey");
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
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Completado).HasColumnName("completado");

                entity.Property(e => e.Kmacumulados).HasColumnName("kmacumulados");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.DeportistaReto)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .HasConstraintName("deportista_reto_usuariodeportista_fkey");

                entity.HasOne(d => d.Reto)
                    .WithMany(p => p.DeportistaReto)
                    .HasForeignKey(d => new { d.Nombrereto, d.Admindeportista })
                    .HasConstraintName("deportista_reto_nombrereto_admindeportista_fkey");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Admindeportista })
                    .HasName("grupo_pkey");

                entity.ToTable("grupo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(30);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.Admindeportista)
                    .HasConstraintName("grupo_admindeportista_fkey");
            });

            modelBuilder.Entity<GrupoCarrera>(entity =>
            {
                entity.HasKey(e => new { e.Nombrecarrera, e.Admincarrera, e.Admingrupo, e.Idgrupo })
                    .HasName("grupo_carrera_pkey");

                entity.ToTable("grupo_carrera");

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Admincarrera)
                    .HasColumnName("admincarrera")
                    .HasMaxLength(20);

                entity.Property(e => e.Admingrupo)
                    .HasColumnName("admingrupo")
                    .HasMaxLength(20);

                entity.Property(e => e.Idgrupo).HasColumnName("idgrupo");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.GrupoCarrera)
                    .HasForeignKey(d => new { d.Idgrupo, d.Admingrupo })
                    .HasConstraintName("grupo_carrera_idgrupo_admingrupo_fkey");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.GrupoCarrera)
                    .HasForeignKey(d => new { d.Nombrecarrera, d.Admincarrera })
                    .HasConstraintName("grupo_carrera_nombrecarrera_admincarrera_fkey");
            });

            modelBuilder.Entity<GrupoDeportista>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Idgrupo, e.Admindeportista })
                    .HasName("grupo_deportista_pkey");

                entity.ToTable("grupo_deportista");

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Idgrupo).HasColumnName("idgrupo");

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.GrupoDeportista)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .HasConstraintName("grupo_deportista_usuariodeportista_fkey");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.GrupoDeportista)
                    .HasForeignKey(d => new { d.Idgrupo, d.Admindeportista })
                    .HasConstraintName("grupo_deportista_idgrupo_admindeportista_fkey");
            });

            modelBuilder.Entity<GrupoReto>(entity =>
            {
                entity.HasKey(e => new { e.Nombrereto, e.Adminreto, e.Admingrupo, e.Idgrupo })
                    .HasName("grupo_reto_pkey");

                entity.ToTable("grupo_reto");

                entity.Property(e => e.Nombrereto)
                    .HasColumnName("nombrereto")
                    .HasMaxLength(30);

                entity.Property(e => e.Adminreto)
                    .HasColumnName("adminreto")
                    .HasMaxLength(20);

                entity.Property(e => e.Admingrupo)
                    .HasColumnName("admingrupo")
                    .HasMaxLength(20);

                entity.Property(e => e.Idgrupo).HasColumnName("idgrupo");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.GrupoReto)
                    .HasForeignKey(d => new { d.Idgrupo, d.Admingrupo })
                    .HasConstraintName("grupo_reto_idgrupo_admingrupo_fkey");

                entity.HasOne(d => d.Reto)
                    .WithMany(p => p.GrupoReto)
                    .HasForeignKey(d => new { d.Nombrereto, d.Adminreto })
                    .HasConstraintName("grupo_reto_nombrereto_adminreto_fkey");
            });

            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.HasKey(e => new { e.Usuariodeportista, e.Nombrecarrera, e.Admincarrera })
                    .HasName("inscripcion_pkey");

                entity.ToTable("inscripcion");

                entity.Property(e => e.Usuariodeportista)
                    .HasColumnName("usuariodeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Nombrecarrera)
                    .HasColumnName("nombrecarrera")
                    .HasMaxLength(30);

                entity.Property(e => e.Admincarrera)
                    .HasColumnName("admincarrera")
                    .HasMaxLength(20);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasMaxLength(10);

                entity.Property(e => e.Recibopago).HasColumnName("recibopago");

                entity.HasOne(d => d.UsuariodeportistaNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.Usuariodeportista)
                    .HasConstraintName("inscripcion_usuariodeportista_fkey");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => new { d.Nombrecarrera, d.Admincarrera })
                    .HasConstraintName("inscripcion_nombrecarrera_admincarrera_fkey");
            });

            modelBuilder.Entity<Patrocinador>(entity =>
            {
                entity.HasKey(e => e.Nombrecomercial)
                    .HasName("patrocinador_pkey");

                entity.ToTable("patrocinador");

                entity.Property(e => e.Nombrecomercial)
                    .HasColumnName("nombrecomercial")
                    .HasMaxLength(30);

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasMaxLength(200);

                entity.Property(e => e.Nombrerepresentante)
                    .HasColumnName("nombrerepresentante")
                    .HasMaxLength(100);

                entity.Property(e => e.Numerotelrepresentante)
                    .HasColumnName("numerotelrepresentante")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Reto>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Admindeportista })
                    .HasName("reto_pkey");

                entity.ToTable("reto");

                entity.HasIndex(e => e.Nombre)
                    .HasName("reto_nombre_key")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(150);

                entity.Property(e => e.Fondoaltitud)
                    .HasColumnName("fondoaltitud")
                    .HasMaxLength(7);

                entity.Property(e => e.Kmtotales).HasColumnName("kmtotales");

                entity.Property(e => e.Periododisponibilidad)
                    .HasColumnName("periododisponibilidad")
                    .HasColumnType("date");

                entity.Property(e => e.Privacidad).HasColumnName("privacidad");

                entity.Property(e => e.Tipoactividad)
                    .HasColumnName("tipoactividad")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AdmindeportistaNavigation)
                    .WithMany(p => p.Reto)
                    .HasForeignKey(d => d.Admindeportista)
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
                    .HasMaxLength(30);

                entity.Property(e => e.Admindeportista)
                    .HasColumnName("admindeportista")
                    .HasMaxLength(20);

                entity.HasOne(d => d.NombrepatrocinadorNavigation)
                    .WithMany(p => p.RetoPatrocinador)
                    .HasForeignKey(d => d.Nombrepatrocinador)
                    .HasConstraintName("reto_patrocinador_nombrepatrocinador_fkey");

                entity.HasOne(d => d.Reto)
                    .WithMany(p => p.RetoPatrocinador)
                    .HasForeignKey(d => new { d.Nombrereto, d.Admindeportista })
                    .HasConstraintName("reto_patrocinador_nombrereto_admindeportista_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
