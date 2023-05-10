using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyAgenda.Controllers
{
  [ApiController]
  [Route("api/")]
  public class ContactController: ControllerBase
  {
    private readonly IContactDAL _contactRepository;
    public ContactController(IContactDAL contactRepository)
    {
        _contactRepository = contactRepository;
    }
    [Route("v1/contacts"), HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(IList<Contact> contacts)
    {
      try
      {
        await _contactRepository.Update(contacts);
        return NoContent();
      }

      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }
  }
}
