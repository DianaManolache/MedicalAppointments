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
            if (!dataContext.MedicalSpecialities.Any())
            {
                var medicalSpecialities = new List<MedicalSpeciality>()
                {
                    new MedicalSpeciality()
                    {
                        Name = "Dermatology"
                    },
                    new MedicalSpeciality()
                    {
                        Name = "Cardiology"
                    },
                    new MedicalSpeciality()
                    {
                        Name = "Pediatrics"
                    }
                };
                dataContext.MedicalSpecialities.AddRange(medicalSpecialities);
                dataContext.SaveChanges();
            }

            if (!dataContext.MedicalExaminations.Any())
            {
                var medicalExaminations = new List<MedicalExamination>()
                {
                    new MedicalExamination()
                    {
                        Doctor = new Doctor()
                        {
                            FirstName = "Elena",
                            LastName = "Martin",
                            PrivateOffice = new PrivateOffice()
                            {
                                City = "Bucuresti",
                                Address = "Bd. Iuliu Maniu, nr. 27A",
                                Phone = 0740213987
                            }
                        },

                        Patient = new Patient()
                        {
                            FirstName = "Diana",
                            LastName = "Manolache",
                            BirthDate = new DateTime(2000, 1, 1)
                        },

                        Time = new DateTime(2024, 3, 27),
                        Duration = 30
                    },


                };
                dataContext.MedicalExaminations.AddRange(medicalExaminations);
                dataContext.SaveChanges();
            }
        }
    }
}