using MedicalAppointments.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace MedicalAppointments.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalExamination> MedicalExaminations { get; set; }
        public DbSet<MedicalSpeciality> MedicalSpecialities { get; set;}
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PrivateOffice> PrivateOffices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicalExamination>()
                        .HasKey(dp => new { dp.DoctorId, dp.PatientId });
            modelBuilder.Entity<MedicalExamination>()
                        .HasOne(d => d.Doctor)
                        .WithMany(dp => dp.MedicalExaminations)
                        .HasForeignKey(d => d.DoctorId);
            modelBuilder.Entity<MedicalExamination>()
                        .HasOne(d => d.Patient)
                        .WithMany(dp => dp.MedicalExaminations)
                        .HasForeignKey(p => p.PatientId);

            modelBuilder.Entity<PrivateOffice>()
                        .HasOne(d => d.Doctor)
                        .WithOne(d => d.Office)
                        .HasForeignKey<PrivateOffice>(d => d.DoctorId)
                        .IsRequired();
        }
    }
}
