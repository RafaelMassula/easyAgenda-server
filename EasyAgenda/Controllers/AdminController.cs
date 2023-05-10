using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgendaBase.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EasyAgenda.Controllers
{
  [ApiController]
  [Route("api/")]
  public class AdminController : ControllerBase
  {
    private readonly IAdminDAL _adminRepository;

    public AdminController(IAdminDAL adminRepository)
    {
      _adminRepository = adminRepository;
    }

    [Route("v1/administrators"), HttpPost]
    public async Task<IActionResult> Register(Admin admin)
    {
      try
      {
        await _adminRepository.Register(admin);
        return Created("", admin);
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
