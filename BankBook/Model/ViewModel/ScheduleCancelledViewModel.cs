namespace EasyAgenda.Model.ViewModel
{
    public class ScheduleCancelledViewModel : ScheduleViewModel
    {
        public DateTime Cancellation { get; set; }
        public string Observation { get; set; }


        public ScheduleCancelledViewModel(string client, string emailClient, string phoneClient, string professional, string emailProfessional, string phoneProfessional,
            DateTime date, TimeSpan start, TimeSpan end, string company, string phoneCompany, string emailCompany, string address, string specialization,
            string description, int customerId, int professionalId, int agendaId, DateTime cancellation, string observation) :
            base(client, emailClient, phoneClient, professional, emailProfessional, phoneProfessional, date, start, end, company, phoneCompany, emailCompany, address,
                specialization, description, customerId, professionalId, agendaId)
        {
            Cancellation = cancellation;
            Observation = observation;
        }

        public ScheduleCancelledViewModel(string client, string professional, string specialization, string description, DateTime date,
            TimeSpan start, TimeSpan end, DateTime cancellation, string observation) : base(client, professional, specialization,
                description, date, start, end)
        {
            Cancellation = cancellation;
            Observation = observation;
        }
    }
}
