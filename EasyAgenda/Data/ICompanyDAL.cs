using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;

namespace EasyAgenda.Data
{
  public interface ICompanyDAL : IQuery<CompanyViewModel>
  {
    public Task Insert(CompanyAddressDTO companyAddress);
    public Task Update(Company company);
    public Task Delete(int id);
  }
}
