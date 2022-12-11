using EasyAgenda.Data.Contracts;
using EasyAgenda.Exceptions;
using EasyAgenda.Model.DTO;
using EasyAgendaService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EasyAgenda.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProfessionalController : ControllerBase
    {
        private readonly IProfessionalDAL _professionalRepository;
        public ProfessionalController(IProfessionalDAL professionalRepository)
        {
            _professionalRepository = professionalRepository;
        }

        [Route("v1/professionals/{id}"), HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var professional = await _professionalRepository.Get(id);
                return Ok(professional);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [Route("v1/professionals"), HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var professional = await _professionalRepository.GetAll();
                return Ok(professional);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [Route("v1/professionals/{id}/schedules"), HttpGet]
        public async Task<IActionResult> GetAppointments(int id)
        {
            try
            {
                var schedulingCustomers = await _professionalRepository.GetAppointments(id);
                return Ok(schedulingCustomers);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [Route("v1/professionals/{id}/schedules/{date}"), HttpGet]
        public async Task<IActionResult> GetAppointmentsByDate(int id, DateTime date)
        {
            try
            {
                var schedulingCustomers = await _professionalRepository.GetAppointmentsByDate(id, date);
                return Ok(schedulingCustomers);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
        [Route("v1/professionals/{id}/schedules/cancelleds"), HttpGet]
        public async Task<IActionResult> GetCanceledAppointments(int id)
        {
            try
            {
                var schedulesCancelled = await _professionalRepository.GetCanceledAppointments(id);
                return Ok(schedulesCancelled);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [Route("v1/professionals/register"), HttpPost]
        public async Task<IActionResult> Register(RecordProfessionalDTO record)
        {
            try
            {
                await _professionalRepository.Register(record);
                return Created("", record);
            }
            catch (CpfException error)
            {
                return BadRequest(error.Message);
            }
            catch (BirthDateException error)
            {
                return BadRequest(error.Message);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [Route("v1/professionals/open/agenda"), HttpPost]
        public async Task<IActionResult> OpenAgenda(IList<AgendaDTO> agendas)
        {
            try
            {
                await _professionalRepository.OpenAgenda(agendas);
                return Created("", agendas);
            }
            catch (TimeoutException error)
            {
                return BadRequest(error.Message);

            }
            catch (ScheduleException error)
            {
                return BadRequest(error.Message);

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
    }
}
