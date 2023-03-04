namespace EasyAgenda.Model.DTO
{
    public class RecordDTO
    {
        public PeopleDTO People { get; set; }
        public UserDTO User { get; set; }
        public ContactDTO Contact { get; set; }

        public RecordDTO(PeopleDTO people, UserDTO user, ContactDTO contact)
        {
            People = people;
            User = user;
            Contact = contact;
        }
    }
}
