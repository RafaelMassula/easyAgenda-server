using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data.Contracts
{
  public interface IAdminDAL
  {
    Task Register(RecordDTO record);
  }
}
