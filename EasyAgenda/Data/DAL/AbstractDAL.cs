using EasyAgenda.Data.Contracts;
using EasyAgenda.Model.ViewModel;

namespace EasyAgenda.Data.DAL
{
  public abstract class AbstractDAL
  {
    public abstract Task<PeopleViewModel> GetByUser(int id);
    public abstract Task<IEnumerable<ScheduleViewModel>> GetAppointmentsByDate(int id, DateTime date);
    public abstract Task<IEnumerable<ScheduleViewModel>> GetAppointments(int id);
    public abstract Task<IEnumerable<ScheduleCancelledViewModel>> GetCanceledAppointments(int id);
  }
}
