using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data.Contracts
{
    public interface IAddressDAL
    {
        Task<AddressDTO> GetAddress(string cep);
    }
}
