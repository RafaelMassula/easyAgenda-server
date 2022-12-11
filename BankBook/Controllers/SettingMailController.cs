using EasyAgenda.Data.Contracts;
using EasyAgenda.ExtensionMethods;
using EasyAgendaBase.Model;
using EasyAgendaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyAgenda.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SettingMailController : ControllerBase
    {
        private readonly ISettingMailDAL _settingMailRepository;
        public SettingMailController(ISettingMailDAL settingMailRepository)
        {
            _settingMailRepository= settingMailRepository;
        }

        [Route("v1/settingMail"), HttpGet]
        public async Task<IActionResult> Get()
        {
           return Ok(await _settingMailRepository.Get());
        }

        [Route("v1/settingmail"), HttpPost]
        public async Task<IActionResult> Configure(SettingMail settingMail)
        {
            try
            {
                await _settingMailRepository.Configure(settingMail); 
                return NoContent();
            }

            catch (DbUpdateException error)
            {
                return BadRequest(error);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
    }
}
