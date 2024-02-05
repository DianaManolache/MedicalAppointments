using AutoMapper;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class PrivateOfficeController : Controller
    {
        private readonly IPrivateOfficeRepository _privateOfficeRepository;
        private readonly IMapper _mapper;

        public PrivateOfficeController(IPrivateOfficeRepository privateOfficeRepository, IMapper mapper)
        {
            _privateOfficeRepository = privateOfficeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PrivateOffice>))]
        public IActionResult GetPrivateOffices()
        {
            var privateOffices = _mapper.Map<List<PrivateOfficeDto>>(_privateOfficeRepository.GetPrivateOffices());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(privateOffices);
        }
        [HttpGet("{PrivateOfficeId}")]
        [ProducesResponseType(200, Type = typeof(PrivateOffice))]
        [ProducesResponseType(400)]
        public IActionResult GetPrivateOffice(Guid PrivateOfficeId)
        {
            if(!_privateOfficeRepository.PrivateOfficeExists(PrivateOfficeId))
            {
                return NotFound();
            }
            var privateOffice = _mapper.Map<PrivateOfficeDto>(_privateOfficeRepository.GetPrivateOffice(PrivateOfficeId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(privateOffice);
        }
        [HttpGet("/Doctors/{DoctorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        public IActionResult GetOfficeByDoctor(Guid DoctorId)
        {
            var office = _mapper.Map<PrivateOfficeDto>(_privateOfficeRepository.GetOfficeByDoctor(DoctorId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(office);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePrivateOffice([FromBody] PrivateOfficeDto privateOfficeCreate)
        {
            if (privateOfficeCreate == null)
                return BadRequest(ModelState);

            var privateOffice = _privateOfficeRepository.GetPrivateOffices()
                .Where(c => c.Address.Trim().ToUpper() == privateOfficeCreate.Address.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (privateOffice != null)
            {
                ModelState.AddModelError("", "Office already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var officeMap = _mapper.Map<PrivateOffice>(privateOfficeCreate);

            if (!_privateOfficeRepository.CreatePrivateOffice(officeMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{PrivateOfficeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePrivateOffice(Guid PrivateOfficeId, [FromBody] PrivateOfficeDto privateOfficeUpdate)
        {
            if (privateOfficeUpdate == null)
                return BadRequest(ModelState);

         /*   if (PrivateOfficeId != privateOfficeUpdate.Id)
                return BadRequest(ModelState); */

            if (!_privateOfficeRepository.PrivateOfficeExists(PrivateOfficeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var officeMap = _mapper.Map<PrivateOffice>(privateOfficeUpdate);

            if (!_privateOfficeRepository.UpdatePrivateOffice(officeMap))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the office {privateOfficeUpdate.Address}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }
        [HttpDelete("{PrivateOfficeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult DeletePrivateOffice(Guid PrivateOfficeId)
        {
            if (!_privateOfficeRepository.PrivateOfficeExists(PrivateOfficeId))
                return NotFound();

            var privateOffice = _privateOfficeRepository.GetPrivateOffice(PrivateOfficeId);

         /*   if (_privateOfficeRepository.GetDoctorsFromOffice(PrivateOfficeId).Count() > 0)
            {
                ModelState.AddModelError("", $"The office {privateOffice.Address} cannot be deleted because it is used by at least one doctor");
                return StatusCode(409, ModelState);
            } */

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_privateOfficeRepository.DeletePrivateOffice(privateOffice))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting the office {privateOffice.Address}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }

    
}
