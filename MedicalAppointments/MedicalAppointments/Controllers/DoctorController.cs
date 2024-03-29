﻿using AutoMapper;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        private readonly ILogger<DoctorController> _logger;
        public DoctorController(ILogger<DoctorController> logger)
        {
            _logger = logger;
        }

        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        [HttpGet(Name = "GetDoctors"), Authorize(Roles = "Admin, User")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Doctor>))]
        public IActionResult GetDoctors(int page = 1, int pageSize = 10)  //Pagination
        {
            var totalCount = _doctorRepository.GetDoctors().Count;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var productsPerPage = _doctorRepository.GetDoctors()
                .Skip((page - 1) * pageSize)    
                .Take(pageSize)
                .ToList();

            return productsPerPage;
        }
    /*    public IActionResult GetDoctors()
        {
            var doctors = _mapper.Map<List<DoctorDto>>(_doctorRepository.GetDoctors());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(doctors);
        }*/

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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDoctor([FromQuery] Guid PatientId, [FromBody] DoctorDto doctorCreate)
        {
            if (doctorCreate == null)
                return BadRequest(ModelState);

            var doctors = _doctorRepository.GetDoctors()
                .Where(c => c.LastName.Trim().ToUpper() == doctorCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (doctors != null)
            {
                ModelState.AddModelError("", "Doctor already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doctorMap = _mapper.Map<Doctor>(doctorCreate);


            if (!_doctorRepository.CreateDoctor(PatientId, doctorMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{doctorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoctor(Guid PacientId, [FromBody] DoctorDto doctorUpdate)
        {
            if (doctorUpdate == null)
                return BadRequest(ModelState);

         /*   if (!_doctorRepository.DoctorExists(doctorUpdate.Id))
                return NotFound(); */

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doctorMap = _mapper.Map<Doctor>(doctorUpdate);

            if (!_doctorRepository.UpdateDoctor(PacientId, doctorMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{doctorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDoctor(Guid doctorId)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
                return NotFound();

            var doctor = _doctorRepository.GetDoctor(doctorId);

            if (!_doctorRepository.DeleteDoctor(doctor))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}
