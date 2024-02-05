using AutoMapper;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalSpecialityController : Controller
    {
        private readonly IMedicalSpecialityRepository _medicalSpecialityRepository;
        private readonly IMapper _mapper;

        public MedicalSpecialityController(IMedicalSpecialityRepository medicalSpecialityRepository, IMapper mapper)
        {
            _medicalSpecialityRepository = medicalSpecialityRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicalSpeciality>))]
        public IActionResult GetMedicalSpecialities()
        {
            var medicalSpecialities = _mapper.Map<List<MedicalSpecialityDto>>(_medicalSpecialityRepository.GetMedicalSpecialities());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(medicalSpecialities);
        }

        [HttpGet("{medicalSpecialityId}")]
        [ProducesResponseType(200, Type = typeof(MedicalSpeciality))]
        [ProducesResponseType(400)]
        public IActionResult GetMedicalSpeciality(Guid medicalSpecialityId)
        {
            if (!_medicalSpecialityRepository.MedicalSpecialityExists(medicalSpecialityId))
            {
                return NotFound();
            }
            var medicalSpeciality = _mapper.Map<MedicalSpecialityDto>(_medicalSpecialityRepository.GetMedicalSpeciality(medicalSpecialityId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(medicalSpeciality);
        }

        [HttpGet("medicalSpeciality/{doctorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(400)]
        public IActionResult GetDoctorBySpeciality(Guid medicalSpecialityId)
        {
            var doctors = _mapper.Map<List<DoctorDto>>(_medicalSpecialityRepository.GetDoctorBySpeciality(medicalSpecialityId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(doctors);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMedicalSpeciality([FromBody] MedicalSpecialityDto medicalSpecialityCreate)
        {
            if (medicalSpecialityCreate == null)
                return BadRequest(ModelState);

            var specility = _medicalSpecialityRepository.GetMedicalSpecialities()
                .Where(c => c.Name.Trim().ToUpper() == medicalSpecialityCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (specility != null)
            {
                ModelState.AddModelError("", "Speciality already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var specialityMap = _mapper.Map<MedicalSpeciality>(medicalSpecialityCreate);

            if (!_medicalSpecialityRepository.CreateMedicalSpeciality(specialityMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
