using EasyAgenda.Model;
using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data.Contracts
{
    public interface IPeopleDAL : IQuery<People>, IManipulation<PeopleDTO>
    {
        Task<int> InsertReturn(PeopleDTO people);
    }
}
