using EasyAgendaService;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EasyAgenda.Model.DTO
{
  public class CompanyDTO
  {
    public int Id { get; set; }
    public required string Description { get; set; }
    private string _cnpj = string.Empty;
    public required string Cnpj { get => this._cnpj; set => _cnpj = CompanyService.CheckedCnpj(value); }
    public required string Email { get; set; } = string.Empty;
    public int StatusId { get; set; }

  }
}
