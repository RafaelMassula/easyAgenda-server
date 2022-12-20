using EasyAgenda.Data;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EasyAgenda.Controllers
{
  [Route("api/")]
  [ApiController]
  public class CompanyController : ControllerBase
  {
    private readonly ICompanyDAL _company;
    public CompanyController(ICompanyDAL company)
    {
      _company = company;
    }

    [Route("v1/companies/{id}"), HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var company = await _company.Get(id);
        return Ok(company);
      }
      catch (SqlException error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
      catch (NullReferenceException error)
      {
        return BadRequest(error.Message);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }
    [Route("v1/companies"), HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        return Ok(await _company.GetAll());
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/companies"), HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create(CompanyAddressDTO companyAddress)
    {
      try
      {
        await _company.Insert(companyAddress);
        return Created("", companyAddress);
      }
      catch (UnauthorizedAccessException error)
      {
        return Unauthorized(error.Message);
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/companies"), HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(Company company)
    {
      try
      {
        await _company.Update(company);
        return NoContent();
      }

      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
      }
    }

    [Route("v1/companies"), HttpDelete]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _company.Delete(id);
        return NoContent();
      }
      catch (NullReferenceException error)
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
