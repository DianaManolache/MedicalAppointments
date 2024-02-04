using AutoMapper;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Doctor>))]
        public IActionResult GetDoctors()
        {
            var doctors = _mapper.Map<List<DoctorDto>>(_doctorRepository.GetDoctors());
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(doctors);
        }

        [HttpGet("{doctorId}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        public IActionResult GetDoctor(Guid doctorId)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
            {
                return NotFound();
            }
            var doctor = _mapper.Map<Doctor>(_doctorRepository.GetDoctor(doctorId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(doctor);
        }
    }
}
