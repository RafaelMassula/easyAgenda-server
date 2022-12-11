namespace EasyAgenda.Model.ViewModel
{
    public class ScheduleViewModel
    {

        public string Client { get; set; } = null!;
        public string EmailClient { get; set; } = null!;
        public string PhoneClient { get; set; } = null!;
        public string Professional { get; set; } = null!;
        public string EmailProfessional { get; set; } = null!;
        public string PhoneProfessional { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Company { get; set; } = null!;
        public string PhoneCompany { get; set; } = null!;
        public string EmailCompany { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CustomerId { get; set; }
        public int AgendaId { get; set; }
        public int ProfessionalId { get; set; }

        public ScheduleViewModel(string professional, string specialization, string description, DateTime date,
            TimeSpan start, TimeSpan end, int agendaId, int professionalId)
        {
            Professional = professional;
            Specialization = specialization;
            Description = description;
            Date = date;
            Start = start;
            End = end;
            AgendaId = agendaId;
            ProfessionalId = professionalId;
        }
        public ScheduleViewModel(string client, string professional, string specialization, string description, DateTime date,
            TimeSpan start, TimeSpan end)
        {
            Professional = professional;
            Specialization = specialization;
            Description = description;
            Date = date;
            Start = start;
            End = end;
            Client = client;
        }
        public ScheduleViewModel(string client, string professional, string specialization, string description, DateTime date,
            TimeSpan start, TimeSpan end, int customerId, int agendaId, int professionalId) : this(client, professional, specialization, description, date, start, end)
        {
            CustomerId = customerId;
            AgendaId = agendaId;
            ProfessionalId = professionalId;
        }
        public ScheduleViewModel(string client, string emailClient, string phoneClient, string professional, string emailProfessional, string phoneProfessional,
            DateTime date, TimeSpan start, string company, string phoneCompany, string emailCompany, string address)
        {
            Client = client;
            EmailClient = emailClient;
            PhoneClient = phoneClient;
            Professional = professional;
            EmailProfessional = emailProfessional;
            PhoneProfessional = phoneProfessional;
            Date = date;
            Start = start;
            Company = company;
            PhoneCompany = phoneCompany;
            EmailCompany = emailCompany;
            Address = address;
        }
        public ScheduleViewModel(string client, string emailClient, string phoneClient, string professional, string emailProfessional, string phoneProfessional,
            DateTime date, TimeSpan start, TimeSpan end, string company, string phoneCompany, string emailCompany, string address, string specialization, string description,
            int customerId, int agendaId, int professionalId) :
            this(client, emailClient, phoneClient, professional, emailProfessional, phoneProfessional, date, start, company, phoneCompany, emailCompany, address)
        {
            End = end;
            Specialization = specialization;
            Description = description;
            CustomerId = customerId;
            AgendaId = agendaId;
            ProfessionalId = professionalId;
        }

    }
}
