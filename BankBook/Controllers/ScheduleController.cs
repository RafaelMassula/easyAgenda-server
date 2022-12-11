using EasyAgenda.Data.Contracts;
using EasyAgenda.Exceptions;
using EasyAgenda.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyAgenda.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleDAL _scheduleRepository;

        public ScheduleController(IScheduleDAL scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [Route("v1/schedules/open/professionals/{id}"), HttpGet()]
        public async Task<IActionResult> GetOpenSchedules(int id)
        {
            try
            {
                var schedulesOpen = await _scheduleRepository.GetSchedulesOpen(id);
                return Ok(schedulesOpen);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }

        }

        [Route("v1/schedules/register/appointment"), HttpPost]
        public async Task<IActionResult> RegisterAppointment(ScheduleDTO schedule)
        {
            try
            {
                await _scheduleRepository.RegisterAppointment(schedule);
                return Created("", schedule);
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

        [Route("v1/schedules/cancel/appointment"), HttpPost]
        public async Task<IActionResult> CancelAppointment(ScheduleCancelledDTO scheduleCancelled)
        {
            try
            {
                await _scheduleRepository.CancelAppointment(scheduleCancelled);
                return Created("", scheduleCancelled);
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
