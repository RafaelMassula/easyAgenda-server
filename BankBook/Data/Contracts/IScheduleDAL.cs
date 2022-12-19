using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;

namespace EasyAgenda.Data.Contracts
{
    public interface IScheduleDAL
    {
        Task<IEnumerable<ScheduleViewModel>> GetSchedulesOpen(int idProfessional);
        Task RegisterAppointment(ScheduleDTO schedule);
        Task ReserveSchedule(ScheduleReservedDTO scheduleReserverd);
        Task CancelAppointment(ScheduleCancelledDTO schedule);
    }
}
