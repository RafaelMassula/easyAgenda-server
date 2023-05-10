using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data
{
  public interface ICompanyDAL : IQuery<Company>
  {
    public Task Insert(Company companyAddress);
    public Task Update(CompanyDTO company);
    public Task Delete(int id);
  }
}
