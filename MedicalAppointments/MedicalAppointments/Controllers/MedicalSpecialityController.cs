﻿using AutoMapper;
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

        [HttpPut("{medicalSpecialityId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMedicalSpeciality(Guid medicalSpecialityId, [FromBody] MedicalSpecialityDto medicalSpecialityUpdate)
        {
            if (medicalSpecialityUpdate == null)
                return BadRequest(ModelState);

          /*  if (medicalSpecialityId != medicalSpecialityUpdate.Id)
                return BadRequest(ModelState); */

            if (!_medicalSpecialityRepository.MedicalSpecialityExists(medicalSpecialityId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var specialityMap = _mapper.Map<MedicalSpeciality>(medicalSpecialityUpdate);

            if (!_medicalSpecialityRepository.UpdateMedicalSpeciality(specialityMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{medicalSpecialityId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult DeleteMedicalSpeciality(Guid medicalSpecialityId)
        {
            if (!_medicalSpecialityRepository.MedicalSpecialityExists(medicalSpecialityId))
                return NotFound();

            var specialityToDelete = _medicalSpecialityRepository.GetMedicalSpeciality(medicalSpecialityId);

            if (_medicalSpecialityRepository.GetDoctorBySpeciality(medicalSpecialityId).Count() > 0)
            {
                ModelState.AddModelError("", $"Speciality {specialityToDelete.Name} cannot be deleted because it is used by at least one doctor");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_medicalSpecialityRepository.DeleteMedicalSpeciality(specialityToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
        
    }
}

