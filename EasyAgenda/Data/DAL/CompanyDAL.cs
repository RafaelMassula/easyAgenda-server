using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;
using EasyAgendaService;
using EasyAgendaBase.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EasyAgenda.ExtensionMethods;

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

    public async Task<CompanyViewModel> Get(int id)
    {
      throw new NotImplementedException();
      //try
      //{
      //  var company = await _context.Companies
      //                    .FirstOrDefaultAsync(c => c.Id == id);


      //  if (company == null)
      //    throw new NullReferenceException("Company not foud");
      //  return company;
      //}
      //catch (SqlException error)
      //{
      //  throw new Exception(error.Message);
      //}
      //catch (NullReferenceException error)
      //{
      //  throw new NullReferenceException(error.Message);
      //}
      //catch (Exception error)
      //{
      //  throw new Exception(error.Message);
      //}
    }

    public async Task<IEnumerable<CompanyViewModel>> GetAll()
    {
      try
      {
        string query = @"SELECT
                             [COMPANIES].[ID],[COMPANIES].[DESCRIPTION], [COMPANIES].[CNPJ],
                             [COMPANIES].[PHONE], [COMPANIES].[EMAIL],
                             [ADDRESSES].[ID], [ADDRESSES].[STREET], [ADDRESSES].[NEIGHBORHOOD],
                             [ADDRESSES].[CITY],[ADDRESSES].[ZIPCODE],[ADDRESSES].[NUMBER],
                             [ADDRESSES].[COMPLEMENT],
                             [STATES].[ID], [STATES].[INITIALS],
                             [STATUS].[ID], [STATUS].[DESCRIPTION]
                         FROM
                             [COMPANIES]
                         INNER JOIN [ADDRESSES]
                            ON [COMPANIES].[ID] = [ADDRESSES].[COMPANYID]
                         INNER JOIN [STATES]
                            ON [STATES].[ID] = [ADDRESSES].[STATEID]
                         INNER JOIN [STATUS]
                            ON [STATUS].[ID] = [COMPANIES].[STATUSID]";

        using var conn = new DbSession(_connectionString);
        return await conn.Connection
          .QueryAsync<CompanyDTO, AddressDTO, State, Status, CompanyViewModel>(query,
          (company, address, state, status) =>
          new CompanyViewModel(company.Id, company.Description, company.Cnpj,
          company.Phone, company.Email,
          new AddressViewModel(address.Street, address.Neighborhood,
          address.City, address.ZipCode, address.Number, address.Complement,
          new State(state.Id, state.Initials)),
          new Status(status.Id, status.Description)), splitOn: "ID, ID, ID, ID");
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

    public async Task Insert(CompanyAddressDTO companyAddress)
    {
      try
      {
        var query = DmlService<CompanyDTO>
            .GetQueryInsertReturn(companyAddress.Company, companyAddress.Company.TableName());

        using var conn = new DbContext(_connectionString);
        int idCompany = (int)await conn.Connection.ExecuteScalarAsync(query, companyAddress.Company);

        companyAddress.Address.CompanyId = idCompany;
        query = DmlService<AddressDTO>
            .GetQueryInsert(companyAddress.Address, companyAddress.Address.TableName());

        await conn.Connection.ExecuteAsync(query, companyAddress.Address);

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

    public async Task Update(Company entity)
    {
      try
      {
        var query = DmlService<Company>.GetQueryUpdate(entity, Company.TableName);
        using var conn = new DbSession(_connectionString);
        await conn.Connection.ExecuteAsync(query, entity);
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
