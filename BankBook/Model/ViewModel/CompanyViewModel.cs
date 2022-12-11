using EasyAgendaBase.Model;

namespace EasyAgenda.Model.ViewModel
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public Status Status { get; set; }

        public CompanyViewModel(Company company, Status status)
        {
            Company = company;
            Status = status;
        }
    }
}
