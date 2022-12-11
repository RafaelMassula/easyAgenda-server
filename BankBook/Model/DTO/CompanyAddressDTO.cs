namespace EasyAgenda.Model.DTO
{
    public class CompanyAddressDTO
    {

        public CompanyDTO Company { get; set; }
        public AddressDTO Address { get; set; }

        public CompanyAddressDTO(CompanyDTO company, AddressDTO address)
        {
            Company = company;
            Address = address;
        }

    }
}
