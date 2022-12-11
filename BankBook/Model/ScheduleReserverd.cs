namespace EasyAgenda.Model
{
    public class ScheduleReserverd
    {
        public int CustomerId { get; set; }
        public int ProfessionalId { get; set; }
        public int AgendaId { get; set; }
        public int StatusId { get; set; }

        public ScheduleReserverd(int customerId, int professionalId, int agendaId, int statusId)
        {
            CustomerId = customerId;
            ProfessionalId = professionalId;
            AgendaId = agendaId;
            StatusId = statusId;
        }

    }
}
