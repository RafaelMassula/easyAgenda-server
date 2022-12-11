using EasyAgendaService;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EasyAgenda.Model.DTO
{
    [Table("ADDRESSES")]
    public class AddressDTO
    {
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public int CompanyId { get; set; }
        public int StateId { get; set; }

        public AddressDTO(string street, string neighborhood, string city, string zipCode,
           string number, string complement, int stateId)
        {
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            ZipCode = AddressService.CheckedCep(zipCode);
            Number = number;
            Complement = complement;
            StateId = stateId;
        }

        public AddressDTO(string street, string neighborhood, string city, string zipCode,
            string number, string complement, int companyId, int stateId) : this(street, neighborhood, city,
                zipCode, number, complement, stateId)
        {
            CompanyId = companyId;
        }
    }
}
