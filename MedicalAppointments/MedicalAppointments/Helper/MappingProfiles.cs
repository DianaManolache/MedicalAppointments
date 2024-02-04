using AutoMapper;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;

namespace MedicalAppointments.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();
            CreateMap<MedicalSpeciality, MedicalSpecialityDto>();
            CreateMap<MedicalSpecialityDto, MedicalSpeciality>();
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();
            CreateMap<PrivateOffice, PrivateOfficeDto>();
            CreateMap<PrivateOfficeDto, PrivateOffice>();
        }
    }
}
