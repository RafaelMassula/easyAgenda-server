using Dapper;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;
using EasyAgendaBase.Exceptions;
using EasyAgendaService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EasyAgenda.Data.DAL
{
  public class CompanyDAL : ICompanyDAL
  {
    private readonly EasyAgendaContext _context;
    private readonly string _connectionString;
    public CompanyDAL(EasyAgendaContext context, IConfiguration configuration)
    {
      _context = context;
      _connectionString = configuration.GetConnectionString("SqlServer");
    }

    public async Task<Company> Get(int id)
    {
      try
      {
        var company = await _context.Companies
               .Include(c => c.Address)
               .ThenInclude(a => a.State)
               .Include(c => c.ContactsCompany)
               .ThenInclude(cc => cc.Contact)
               .FirstOrDefaultAsync(c => c.Id == id);


        if (company == null)
          throw new NullReferenceException("Company not foud");
        return company;
      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
      catch (NullReferenceException error)
      {
        throw new NullReferenceException(error.Message);
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
    }

    public async Task<IEnumerable<Company>> GetAll()
    {
      try
      {
        return await (from companies in _context.Companies
               .Include(c => c.Address)
               .ThenInclude(a => a.State)
               .Include(c => c.ContactsCompany)
               .ThenInclude(cc => cc.Contact)
                      select companies)
               .ToListAsync();

      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
    }

    public async Task Insert(Company companyAddress)
    {
      try
      {
        _context.Add(companyAddress);
        await _context.SaveChangesAsync();
      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
    }

    public async Task Update(CompanyDTO company)
    {
      try
      {
        var companyChange = new Company(company.Id, company.Description, company.Cnpj, company.Email, company.StatusId);

        _context.Update(companyChange);
        await _context.SaveChangesAsync();
      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
    }

    public async Task Delete(int id)
    {
      throw new NotImplementedException();
      //try
      //{
      //  var companyViewModel = await Get(id);
      //  _context.Companies.Remove(companyViewModel);
      //  await _context.SaveChangesAsync();
      //}
      //catch (SqlException error)
      //{
      //  throw new Exception(error.Message);
      //}
      //catch (Exception error)
      //{
      //  throw new Exception(error.Message);
      //}
    }
  }
}
