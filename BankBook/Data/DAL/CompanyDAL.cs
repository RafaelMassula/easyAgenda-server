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
        private readonly string _stringConnection;
        public CompanyDAL(EasyAgendaContext context, IConfiguration configuration)
        {
            _context = context;
            _stringConnection = configuration.GetConnectionString("SqlServer");
        }

        public async Task<CompanyViewModel> Get(int id)
        {
            try
            {
                var company = await _context.Companies
                    .Join(_context.Status, company => company.StatusId, status => status.Id,
                    (company, status) => new CompanyViewModel(company, status) { Company = company, Status = status })
                    .FirstOrDefaultAsync(companyViewModel => companyViewModel.Company.Id == id);

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

        public async Task<IEnumerable<CompanyViewModel>> GetAll()
        {
            try
            {
                return await _context.Companies
                    .Join(_context.Status, company => company.StatusId, status => status.Id,
                    (company, status) => new CompanyViewModel(company, status))
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

        public async Task Insert(CompanyAddressDTO companyAddress)
        {
            try
            {
                var query = DmlService<CompanyDTO>
                    .GetQueryInsertReturn(companyAddress.Company, companyAddress.Company.TableName());

                using var conn = new DbContext(_stringConnection);
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
                using var conn = new DbSession(_stringConnection);
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
            try
            {
                var companyViewModel = await Get(id);
                _context.Companies.Remove(companyViewModel.Company);
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
    }
}
