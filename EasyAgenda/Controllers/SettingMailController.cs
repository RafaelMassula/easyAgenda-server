using EasyAgenda.Data.Contracts;
using EasyAgendaBase.Model;
using EasyAgendaService.Contracts;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace EasyAgenda.Controllers
{
  [Route("api/")]
  [ApiController]
  public class SettingMailController : ControllerBase
  {
    private readonly ISettingMailDAL _settingMailRepository;
    private readonly IEmailService _emailService;
    public SettingMailController(ISettingMailDAL settingMailRepository, IEmailService emailService)
    {
      _settingMailRepository = settingMailRepository;
      _emailService = emailService;
    }

    [Route("v1/settingmail"), HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Get()
    {
      return Ok(await _settingMailRepository.Get());
    }

    [Route("v1/settingmail"), HttpPost]
    [Authorize(Roles = "ADMIN")]
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
    [Route("v1/settingmail/test"), HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> TestSendEmail(SettingMail settingMail)
    {
      try
      {
        await _emailService.TestSend(settingMail);
        return Ok();
      }

      catch (AuthenticationException error)
      {
        return BadRequest(error.Message);
      }
      catch(SslHandshakeException error)
      {
        return BadRequest(error.Message);
      }
      catch(SocketException error)
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
