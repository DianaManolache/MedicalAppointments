﻿using AutoMapper;
using MedicalAppointments.Data;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;

namespace MedicalAppointments.Service.PrivateOfficeServices
{
    public class PrivateOfficeRepository : IPrivateOfficeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PrivateOfficeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool PrivateOfficeExists(Guid privateOfficeId)
        {
            return _context.PrivateOffices.Any(p => p.Id == privateOfficeId);
        }
        public ICollection<PrivateOffice> GetPrivateOffices()
        {
            return _context.PrivateOffices.ToList();
        }
        public PrivateOffice GetPrivateOffice(Guid privateOfficeId)
        {
            return _context.PrivateOffices.FirstOrDefault(p => p.Id == privateOfficeId);
        }
        public PrivateOffice GetOfficeByDoctor(Guid doctorId)
        {
            return _context.Doctors.Where(o => o.Id == doctorId).Select(d => d.PrivateOffice).FirstOrDefault();
        }

        public bool CreatePrivateOffice(PrivateOffice privateOffice)
        {
            _context.Add(privateOffice);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdatePrivateOffice(PrivateOffice privateOffice)
        {
            var updated = _context.PrivateOffices.Update(privateOffice);
            return Save();
        }

        public bool DeletePrivateOffice(PrivateOffice privateOffice)
        {
            _context.PrivateOffices.Remove(privateOffice);
            return Save();
        }
    }
}