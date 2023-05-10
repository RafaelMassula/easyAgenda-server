using EasyAgenda.Data.Contracts;
using EasyAgenda.Exceptions;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgendaBase.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "ADMIN,CLIENT")]
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

    [Route("v1/professionals/users/{id}"), HttpGet]
    [Authorize(Roles = "ADMIN,PROFESSIONAL")]
    public async Task<IActionResult> GetByuser(int id)
    {
      try
      {
        var professional = await _professionalRepository.GetByUser(id);
        return Ok(professional);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/professionals"), HttpGet]
    [Authorize(Roles = "ADMIN,CLIENT")]
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
    [Authorize(Roles = "ADMIN,PROFESSIONAL")]
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
    [Authorize(Roles = "ADMIN,PROFESSIONAL")]
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
    [Authorize(Roles = "ADMIN,PROFESSIONAL")]
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

    [Route("v1/professionals"), HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Register(Professional professional)
    {
      try
      {
        await _professionalRepository.Register(professional);
        return Created("", professional);
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
    [Authorize(Roles = "PROFESSIONAL")]
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
