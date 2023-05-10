using System.Text.RegularExpressions;


namespace EasyAgendaService.ExtesionMethods
{
  internal static class ExtensionMethods
  {
    public static string AplyMasckCep(this string cep)
    {
      string pattern = @"(^[0-9]{5})([0-9]{3})";
      var regex = new Regex(pattern);

      return string.Concat(regex.Match(cep).Groups[1].Value, "-", regex.Match(cep).Groups[2].Value);
    }
    public static string ApplyMasckPhone(this string phone)
    {
      string pattern;
      if (phone.Length == 11)
        pattern = @"(^[0-9]{2})([0-9]{5})([0-9]{4})";
      else
        pattern = @"(^[0-9]{2})([0-9]{4})([0-9]{4})";

      var regex = new Regex(pattern);
      return string.Concat("(", regex.Match(phone).Groups[1], ")", regex.Match(phone).Groups[2], "-", regex.Match(phone).Groups[3]);
    }
  }
}
