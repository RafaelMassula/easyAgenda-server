using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;

namespace EasyAgenda.Data.Contracts
{
  public interface IProfessionalDAL : IQuery<Professional>
  {
    Task<PeopleViewModel> GetByUser(int id);
    Task<IEnumerable<ScheduleViewModel>> GetAppointmentsByDate(int id, DateTime date);
    Task<IEnumerable<ScheduleViewModel>> GetAppointments(int id);
    Task<IEnumerable<ScheduleCancelledViewModel>> GetCanceledAppointments(int id);
    Task OpenAgenda(IList<AgendaDTO> agenda);
    Task Register(Professional professional);
  }
}
