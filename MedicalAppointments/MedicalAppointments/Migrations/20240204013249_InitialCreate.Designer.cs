﻿// <auto-generated />
using System;
using MedicalAppointments.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicalAppointments.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240204013249_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedicalAppointments.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedicalSpecialityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicalSpecialityId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MedicalAppointments.Models.MedicalExamination", b =>
                {
                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("DoctorId", "PatientId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalExaminations");
                });

            modelBuilder.Entity("MedicalAppointments.Models.MedicalSpeciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalSpecialities");
                });

            modelBuilder.Entity("MedicalAppointments.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MedicalAppointments.Models.PrivateOffice", b =>
                {
                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.ToTable("PrivateOffices");
                });

            modelBuilder.Entity("MedicalAppointments.Models.Doctor", b =>
                {
                    b.HasOne("MedicalAppointments.Models.MedicalSpeciality", "MedicalSpeciality")
                        .WithMany("Doctors")
                        .HasForeignKey("MedicalSpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalSpeciality");
                });

            modelBuilder.Entity("MedicalAppointments.Models.MedicalExamination", b =>
                {
                    b.HasOne("MedicalAppointments.Models.Doctor", "Doctor")
                        .WithMany("MedicalExaminations")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalAppointments.Models.Patient", "Patient")
                        .WithMany("MedicalExaminations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicalAppointments.Models.PrivateOffice", b =>
                {
                    b.HasOne("MedicalAppointments.Models.Doctor", "Doctor")
                        .WithOne("PrivateOffice")
                        .HasForeignKey("MedicalAppointments.Models.PrivateOffice", "DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("MedicalAppointments.Models.Doctor", b =>
                {
                    b.Navigation("MedicalExaminations");

                    b.Navigation("PrivateOffice")
                        .IsRequired();
                });

            modelBuilder.Entity("MedicalAppointments.Models.MedicalSpeciality", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("MedicalAppointments.Models.Patient", b =>
                {
                    b.Navigation("MedicalExaminations");
                });
#pragma warning restore 612, 618
        }
    }
}