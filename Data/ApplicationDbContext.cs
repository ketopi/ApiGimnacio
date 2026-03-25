using Microsoft.EntityFrameworkCore;
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor que recibe la configuración de la base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet representa una tabla en la base de datos
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }

        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Pago> Pagos { get; set; }




        // Método para configurar reglas adicionales del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llama a la configuración base de EF Core
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Permisos>().HasIndex(p => p.Nombre).IsUnique(); // Para asegurar que el nombre de Permisos sea único.
            modelBuilder.Entity<Permisos>().Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();

            modelBuilder.Entity<Membresia>().HasIndex(m => m.Nombre).IsUnique(); // Para asegurar que el nombre de Membresia sea único.
            modelBuilder.Entity<Membresia>().Property(x => x.TipoPlan).HasConversion<string>();

            modelBuilder.Entity<Cliente>().HasIndex(c => c.CI).IsUnique(); // para asegurar el carnet de identidad sea unico
            modelBuilder.Entity<Cliente>().Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Roles>().Property(r => r.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd(); //pone fecha aumtomaticamente
            modelBuilder.Entity<Roles>().HasIndex(r => r.Nombre).IsUnique(); //UNICO NOMBRE DE ROL (ADMIN , CAJERO ETC.)

            modelBuilder.Entity<RolPermiso>().HasKey(rp => new { rp.RolId, rp.PermisoId }); //Registro unico por la combinacion  de Rol + Permiso
            modelBuilder.Entity<RolPermiso>().HasOne(rp => rp.Rol).WithMany(r => r.RolPermisos).HasForeignKey(rp => rp.RolId); //Relacion   ver todos los permisos de un roles
            modelBuilder.Entity<RolPermiso>().HasOne(rp => rp.Permiso).WithMany(p => p.RolPermisos).HasForeignKey(rp => rp.PermisoId);// La tabla RolPermiso tiene una relación donde cada registro pertenece a un solo Permiso

            modelBuilder.Entity<Empleado>().Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Usuario>().HasOne(u => u.Empleado).WithOne(e => e.Usuario);// Usuario - Empleado (1:1)           
            modelBuilder.Entity<Usuario>().HasOne(u => u.Rol).WithMany(r => r.Usuarios).HasForeignKey(u => u.RolId); // Usuario - Rol (N:1)            
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique();// Username único           
            modelBuilder.Entity<Usuario>().Property(u => u.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd(); // CreatedAt automático


            // Fecha de creación con valor por defecto
            modelBuilder.Entity<Suscripcion>().Property(s => s.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            // Índice para acelerar consultas por Cliente y Membresía
            modelBuilder.Entity<Suscripcion>().HasIndex(s => new { s.ClienteId, s.MembresiaId });
            // Estado activo por defecto
            modelBuilder.Entity<Suscripcion>().Property(s => s.Estado).HasDefaultValue(true);
            // Relación: Suscripcion -> Cliente (muchos a uno)
            modelBuilder.Entity<Suscripcion>().HasOne(s => s.Cliente)
                .WithMany(c => c.Suscripciones) // Un cliente tiene muchas suscripciones
                .HasForeignKey(s => s.ClienteId);

            // Relación: Suscripcion -> Membresia (muchos a uno)
            modelBuilder.Entity<Suscripcion>()
                .HasOne(s => s.Membresia)
                .WithMany(m => m.Suscripciones) // Una membresía tiene muchas suscripciones
                .HasForeignKey(s => s.MembresiaId)
                .OnDelete(DeleteBehavior.Restrict); // No permite borrar membresía en uso



            modelBuilder.Entity<Asistencia>()   //Asistencia → Suscripción → Cliente
                .HasOne(a => a.Suscripcion)
                .WithMany(s => s.Asistencias)
                .HasForeignKey(a => a.SuscripcionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.RegistradoPor)
                .WithMany(u => u.AsistenciasRegistradas)
                .HasForeignKey(a => a.RegistradoPorId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Suscripcion)
                .WithMany(s => s.Pagos)
                .HasForeignKey(p => p.SuscripcionId)
                .OnDelete(DeleteBehavior.Cascade);





        }
    }
}