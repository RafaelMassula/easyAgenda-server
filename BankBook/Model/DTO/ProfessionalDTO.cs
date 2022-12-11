using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
    [Table("[PROFESSIONALS]")]
    public class ProfessionalDTO
    {
        public int PeopleId { get; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public ProfessionalDTO(int peopleId, int userId, int companyId)
        {
            PeopleId = peopleId;
            UserId = userId;
            CompanyId = companyId;
        }
    }
}
