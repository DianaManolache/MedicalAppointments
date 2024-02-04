using MedicalAppointments.Data;
using MedicalAppointments.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalAppointments
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.MedicalExaminations.Any())
            {
                var doctor = new Doctor()
                {
                    FirstName = "Elena",
                    LastName = "Martin",
                    MedicalSpeciality = new MedicalSpeciality()
                    {
                        Name = "General Medicine"
                    },
                    PrivateOffice = new PrivateOffice()
                    {
                        City = "Bucuresti",
                        Address = "Bd. Iuliu Maniu, nr. 27A",
                        Phone = "0740213987"
                    }
                };

                dataContext.Doctors.Add(doctor);
                dataContext.SaveChanges();

                var medicalExaminations = new List<MedicalExamination>()
                    {
                        new MedicalExamination()
                        {
                            Doctor = doctor,
                            Patient = new Patient()
                            {
                                FirstName = "Diana",
                                LastName = "Manolache",
                                BirthDate = new DateTime(2000, 1, 1)
                            },
                            Time = new DateTime(2024, 3, 27),
                            Duration = 30
                        }
                    };

                dataContext.MedicalExaminations.AddRange(medicalExaminations);
                dataContext.SaveChanges();
            }
        }
    }
}