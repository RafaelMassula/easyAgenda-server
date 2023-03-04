using EasyAgenda.Data.Contracts;
using EasyAgenda.Model.DTO;
using EasyAgendaService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EasyAgenda.Controllers
{
  [ApiController]
  [Route("api/")]
  public class AdminController: ControllerBase
  {
    private readonly IAdminDAL _adminRepository;

    public AdminController(IAdminDAL adminRepository)
    {
      _adminRepository = adminRepository;
    }

    [Route("v1/administrators"), HttpPost]
    public async Task<IActionResult> Register(RecordDTO record)
    {
      try
      {
        await _adminRepository.Register(record);
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
