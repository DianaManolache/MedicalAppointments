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
    }

    
}
