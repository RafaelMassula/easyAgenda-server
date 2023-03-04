using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;

namespace EasyAgenda.Data.Contracts
{
  public interface ICustomerDAL: IQuery<Customer>
  {
    Task<PeopleViewModel> GetByUser(int id);
    Task<IEnumerable<ScheduleViewModel>> GetAppointmentsByDate(int id, DateTime date);
    Task<IEnumerable<ScheduleViewModel>> GetAppointments(int id);
    Task<IEnumerable<ScheduleCancelledViewModel>> GetCanceledAppointments(int id);
    Task Register(RecordDTO record);
  }
}
