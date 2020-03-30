namespace SistemaControlMedico.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SistemaMedicoDbContext : DbContext
    {
        public SistemaMedicoDbContext()
            : base("name=SistemaMedicoDbContext")
        {
        }

        public virtual DbSet<Altas> Altas { get; set; }
        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<Habitaciones> Habitaciones { get; set; }
        public virtual DbSet<Ingresos> Ingresos { get; set; }
        public virtual DbSet<Medicos> Medicos { get; set; }
        public virtual DbSet<Pacientes> Pacientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habitaciones>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Habitaciones>()
                .HasMany(e => e.Ingresos)
                .WithRequired(e => e.Habitaciones)
                .HasForeignKey(e => e.habitacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ingresos>()
                .HasMany(e => e.Altas)
                .WithRequired(e => e.Ingresos)
                .HasForeignKey(e => e.ingreso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Medicos>()
                .Property(e => e.exequatur)
                .IsUnicode(false);

            modelBuilder.Entity<Medicos>()
                .Property(e => e.especialidad)
                .IsUnicode(false);

            modelBuilder.Entity<Medicos>()
                .HasMany(e => e.Citas)
                .WithRequired(e => e.Medicos)
                .HasForeignKey(e => e.medico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicos>()
                .HasMany(e => e.Ingresos)
                .WithRequired(e => e.Medicos)
                .HasForeignKey(e => e.medico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pacientes>()
                .Property(e => e.cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Pacientes>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Pacientes>()
                .HasMany(e => e.Citas)
                .WithRequired(e => e.Pacientes)
                .HasForeignKey(e => e.paciente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pacientes>()
                .HasMany(e => e.Ingresos)
                .WithRequired(e => e.Pacientes)
                .HasForeignKey(e => e.paciente)
                .WillCascadeOnDelete(false);
        }
    }
}
