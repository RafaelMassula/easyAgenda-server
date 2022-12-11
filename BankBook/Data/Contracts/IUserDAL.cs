using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data.Contracts
{
    public interface IUserDAL
    {
        Task<int> InsertReturn(UserDTO user);
    }
}
