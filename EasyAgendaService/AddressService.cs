using EasyAgendaBase.Exceptions;
using System.Text.RegularExpressions;

namespace EasyAgendaService
{
  public abstract class AddressService
  {
    private readonly static List<string> _invalidNumbersOfCep = new()
        {
                "00000-00",
                "11111-11",
                "22222-22",
                "33333-33",
                "44444-44",
                "55555-55",
                "66666-66",
                "77777-77",
                "88888-88",
                "99999-99"
        };
    public static string CheckedCep(string cep)
    {
      if (_invalidNumbersOfCep.Contains(cep) || string.IsNullOrEmpty(cep))
      {
        throw new CpfException("Invalid cep.");
      }

      return Regex.Replace(cep, @"[A-Za-z-\W]+", "");
    }
  }
}
