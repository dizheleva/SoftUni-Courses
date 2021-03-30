using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> Prescriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Hospital;Integrated Security=True");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(x =>
            {
                x.HasKey(p => p.PatientId);

                x.Property(p => p.FirstName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.Property(p => p.LastName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.Property(p => p.Address)
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.Property(p => p.Email)
                    .HasMaxLength(80)
                    .IsRequired(true)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Doctor>(x =>
            {
                x.HasKey(d => d.DoctorId);

                x.Property(d => d.Name)
                    .HasMaxLength(100)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.Property(d => d.Specialty)
                    .HasMaxLength(100)
                    .IsRequired(true)
                    .IsUnicode(true);

            });

            modelBuilder.Entity<Visitation>(x =>
            {
                x.HasKey(v => v.VisitationId);

                x.Property(v => v.Date)
                    .IsRequired(true);

                x.Property(v => v.Comments)
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.HasOne(v => v.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(v => v.PatientId);

                x.HasOne(v => v.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(v => v.DoctorId);
            });

            modelBuilder.Entity<Diagnose>(x =>
            {
                x.HasKey(d => d.DiagnoseId);

                x.Property(d => d.Name)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.Property(d => d.Comments)
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.HasOne(d => d.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PatientId);
            });

            modelBuilder.Entity<Medicament>(x =>
            {
                x.HasKey(m => m.MedicamentId);

                x.Property(m => m.Name)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<PatientMedicament>(x => { x.HasKey(y => new {y.PatientId, y.MedicamentId}); });

            base.OnModelCreating(modelBuilder);
        }
    }
}
