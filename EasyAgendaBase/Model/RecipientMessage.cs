
namespace EasyAgendaBase.Model
{
  public class RecipientMessage
  {
    public string NameClient { get; set; } = String.Empty;
    public string EmailClient { get; set; } = String.Empty;
    public TimeSpan Start { get; set; }
    public DateTime Date { get; set; }
    public string NameProfessional { get; set; } = String.Empty;
    public string EmailProfessional { get; set; } = String.Empty;
    public string Company { get; set; } = String.Empty;
    public string PhoneCompany { get; set; } = String.Empty;
    public string EmailCompany { get; set; } = String.Empty;
    public string CompanyAddress { get; set; } = String.Empty;
    public string LinkConfirm { get; set; } = String.Empty;

    public RecipientMessage(string nameClient, string emailClient)
    {
      NameClient = nameClient;
      EmailClient = emailClient;
    }

    public RecipientMessage(string nameClient, string emailClient, string company, string phoneCompany,
        string emailCompany) : this(nameClient, emailClient)
    {
      Company = company;
      PhoneCompany = phoneCompany;
      EmailCompany = emailCompany;
    }

    public RecipientMessage(string nameClient, string emailClient, DateTime date, TimeSpan start,
        string nameProfessional, string emailProfessional, string company, string phoneCompany, string emailCompany, string companyAddress) :
        this(nameClient, emailClient, company, phoneCompany, emailCompany)
    {
      Date = date;
      Start = start;
      NameProfessional = nameProfessional;
      EmailProfessional = emailProfessional;
      CompanyAddress = companyAddress;
    }

    public RecipientMessage(string nameClient, string emailClient, DateTime date, TimeSpan start,
        string nameProfessional, string emailProfessional, string company, string phoneCompany, string emailCompany, string companyAddress,
        string linkConfirm) :
        this(nameClient, emailClient, date, start, nameProfessional, emailProfessional, company, phoneCompany, emailCompany,
            companyAddress)
    {
      LinkConfirm = linkConfirm;
    }
  }
}
