using EasyAgendaBase.Model;
using EasyAgendaService;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("ADDRESSES")]
    public class Address
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("STREET")]
        public string Street { get; set; }
        [Column("NEIGHBORHOOD")]
        public string Neighborhood { get; set; }
        [Column("CITY")]
        public string City { get; set; }
        [Column("ZIPCODE")]
        public string ZipCode { get; set; }
        [Column("NUMBER")]
        public string Number { get; set; }
        [Column("COMPLEMENT")]
        public string Complement { get; set; }
        [Column("COMPANYID")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;
        [Column("STATEID")]
        public int StateId { get; set; }
        public virtual State State { get; set; } = null!;

        public Address(int id, string street, string neighborhood, string city, string zipCode,
            string number, string complement, int companyId, int stateId)
        {
            Id = id;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            ZipCode = AddressService.CheckedCep(zipCode);
            Number = number;
            Complement = complement;
            CompanyId = companyId;
            StateId = stateId;
        }
    }
}
