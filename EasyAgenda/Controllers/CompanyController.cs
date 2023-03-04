using EasyAgenda.Data;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;
using EasyAgendaService;
using EasyAgendaService.Contracts;
using EasyAgendaService.Utilities;
using EasyAgendaService.Utilities.Helpers;
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
    private readonly IUriService _uriService;
    public CompanyController(ICompanyDAL company, IUriService uriService)
    {
      _company = company;
      _uriService = uriService;
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
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
    {
      try
      {
        var route = Request.Path.Value;
        var pageDataService = new PageDataService<CompanyViewModel>(filter.PageNumber, filter.PageSize);

        var compaies = await _company.GetAll();
        var pageData = pageDataService.GetDataPage(compaies).ToList();
        var totalRecords = pageData.Count;
        var pagedResponse = PaginationHelper.CreatePagedResponse(pageData, pageDataService.ValidFilter, totalRecords, _uriService, route);
        
        return Ok(pagedResponse);
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
