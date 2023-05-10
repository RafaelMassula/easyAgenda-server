using EasyAgenda.Model;
using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data.Contracts
{
  public interface IAddressDAL
  {
    Task<IEnumerable<State>> GetStates();
    Task<AddressDTO> GetAddress(string cep);
    Task Update(Address address);
  }
}
