﻿using MedicalAppointments.Data;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;

namespace MedicalAppointments.Repository
{
    public class MedicalSpecialityRepository : IMedicalSpecialityRepository
    {
        private readonly DataContext _context;
        public MedicalSpecialityRepository(DataContext context)
        {
            _context = context;
        }

        public MedicalSpeciality GetMedicalSpeciality(Guid medicalSpecialityId)
        {
            return _context.MedicalSpecialities.Where(d => d.Id == medicalSpecialityId).FirstOrDefault();
        }
        public ICollection<MedicalSpeciality> GetDoctorBySpeciality(Guid medicalSpecialityId)
        {
            //make this using select from
            var doctors = from doctor in _context.Doctors
                          join medicalSpeciality in _context.MedicalSpecialities
                          on doctor.MedicalSpecialityId equals medicalSpeciality.Id
                          where medicalSpeciality.Id == medicalSpecialityId
                          select doctor;
            return (ICollection<MedicalSpeciality>)doctors;

        }
        public ICollection<MedicalSpeciality> GetMedicalSpecialities()
        {
            return _context.MedicalSpecialities.OrderBy(d => d.Id).ToList();
        }
        public bool MedicalSpecialityExists(Guid medicalSpecialityId)
        {
            return _context.MedicalSpecialities.Any(d => d.Id == medicalSpecialityId);

        }

        public bool CreateMedicalSpeciality(MedicalSpeciality medicalSpeciality)
        {
            _context.Add(medicalSpeciality);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateMedicalSpeciality(MedicalSpeciality medicalSpeciality)
        {
            _context.Update(medicalSpeciality);
            return Save();
        }

        public bool DeleteMedicalSpeciality(MedicalSpeciality medicalSpeciality)
        {
            _context.MedicalSpecialities.Remove(medicalSpeciality);
            return Save();
        }
    }
}
