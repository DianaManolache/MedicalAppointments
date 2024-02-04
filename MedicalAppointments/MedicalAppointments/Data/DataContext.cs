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
        public DbSet<User> Users { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //primary keys (id)
            modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
            modelBuilder.Entity<MedicalSpeciality>().HasKey(m => m.Id);
            modelBuilder.Entity<Patient>().HasKey(p => p.Id);
            modelBuilder.Entity<PrivateOffice>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasKey(p => p.Id);

            //one to one relationship (doctor - private office)
            modelBuilder.Entity<Doctor>()
                .HasOne(doctor => doctor.PrivateOffice)
                .WithOne(privateOffice => privateOffice.Doctor)
                .HasForeignKey<PrivateOffice>(privateOffice => privateOffice.DoctorId);

            //one to many relationship (doctor - medical speciality)
            modelBuilder.Entity<Doctor>()
                .HasOne(doctor => doctor.MedicalSpeciality)
                .WithMany(medicalSpeciality => medicalSpeciality.Doctors)
                .HasForeignKey(doctor => doctor.MedicalSpecialityId);

            //many to many relationship (doctor - patient ==> medical examination)
            modelBuilder.Entity<MedicalExamination>()
                .HasKey(k => new { k.DoctorId, k.PatientId });
            modelBuilder.Entity<MedicalExamination>()
                .HasOne(medicalExamination => medicalExamination.Doctor)
                .WithMany(doctor => doctor.MedicalExamination)
                .HasForeignKey(medicalExamination => medicalExamination.DoctorId);
            modelBuilder.Entity<MedicalExamination>()
                .HasOne(medicalExamination => medicalExamination.Patient)
                .WithMany(patient => patient.MedicalExamination)
                .HasForeignKey(medicalExamination => medicalExamination.PatientId);

            base.OnModelCreating(modelBuilder);

        }
    }
}
