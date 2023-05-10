using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EasyAgenda.Controllers
{
  [ApiController]
  [Route("api/")]
  public class AddressController : ControllerBase
  {
    private readonly IAddressDAL _addressRepository;

    public AddressController(IAddressDAL addressRepository)
    {
      _addressRepository = addressRepository;
    }

    [Route("v1/addresses/states"), HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetStates()
    {
      try
      {
        var states = await _addressRepository.GetStates();
        return Ok(states);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/addresses/{cep}"), HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAddress(string cep)
    {
      try
      {
        var address = await _addressRepository.GetAddress(cep);
        return Ok(address);
      }
      catch (HttpRequestException error)
      {
        return BadRequest(error.Message);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }
    [Route("v1/addresses"), HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update([FromBody]Address address)
    {
      try
      {
        await _addressRepository.Update(address);
        return NoContent();
      }

      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }
  }
}
