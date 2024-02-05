using AutoMapper;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientController(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patient>))]
        public IActionResult Getpatients()
        {
            var patients = _mapper.Map<List<PatientDto>>(_patientRepository.GetPatients());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patients);
        }
        [HttpGet("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        public IActionResult GetPatient(Guid patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var patient = _mapper.Map<PatientDto>(_patientRepository.GetPatient(patientId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patient);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient([FromQuery] Guid DoctorId, [FromBody] PatientDto patientCreate)
        {
            if (patientCreate == null)
                return BadRequest(ModelState);

            var patient = _patientRepository.GetPatients()
                .Where(c => c.LastName.Trim().ToUpper() == patientCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (patient != null)
            {
                ModelState.AddModelError("", "Patient already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientMap = _mapper.Map<Patient>(patientCreate);

            if (!_patientRepository.CreatePatient(DoctorId, patientMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
