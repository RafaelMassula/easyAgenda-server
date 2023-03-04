using EasyAgenda.Data.Contracts;
using EasyAgenda.Model.DTO;
using EasyAgendaService.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyAgenda.Controllers
{
  [Route("api/")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ICustomerDAL _customerRepository;

    public CustomerController(ICustomerDAL customerRepository)
    {
      _customerRepository = customerRepository;
    }

    [Route("v1/customers/users/{id}"), HttpGet]
    [Authorize(Roles = "ADMIN,CLIENT")]
    public async Task<IActionResult> GetByUser(int id)
    {
      try
      {
        var customer = await _customerRepository.GetByUser(id);
        return Ok(customer);
      }
      catch (KeyNotFoundException error)
      {
        return NotFound(error.Message);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/customers"), HttpGet]
    [Authorize(Roles = "ADMIN,CLIENT")]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var customers = await _customerRepository.GetAll();
        return Ok(customers);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/customers/{id}/schedules"), HttpGet]
    [Authorize(Roles = "CLIENT")]
    public async Task<IActionResult> GetAppointments(int id)
    {
      try
      {
        var schedulingProfessionals = await _customerRepository.GetAppointments(id);
        return Ok(schedulingProfessionals);
      }
      catch (DbUpdateException error)
      {
        return BadRequest(error.Message);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/customers/{id}/schedules/{date}"), HttpGet]
    [Authorize(Roles = "CLIENT")]
    public async Task<IActionResult> GetAppointmentsByDate(int id, DateTime date)
    {
      try
      {
        var schedulingProfessionals = await _customerRepository.GetAppointmentsByDate(id, date);
        return Ok(schedulingProfessionals);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/customers/{id}/schedules/cancelled"), HttpGet]
    [Authorize(Roles = "CLIENT")]
    public async Task<IActionResult> GetCanceledAppointments(int id)
    {
      try
      {
        var schedulesCancelled = await _customerRepository.GetCanceledAppointments(id);
        return Ok(schedulesCancelled);
      }
      catch (DbUpdateException error)
      {
        return BadRequest(error.Message);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/customers/register"), HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RecordDTO record)
    {
      try
      {
        await _customerRepository.Register(record);
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
  }
}
