namespace EasyAgenda.Model.DTO
{
    public class RecordProfessionalDTO : RecordDTO
    {
        public int CompanyId { get; set; }

        public RecordProfessionalDTO(PeopleDTO people, UserDTO user, ContactDTO contact, int companyId) :
            base(people, user, contact)
        {
            CompanyId = companyId;
        }
    }
}
