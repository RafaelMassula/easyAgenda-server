using EasyAgendaService.Exceptions;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EasyAgendaService
{
    public class CompanyService
    {
        private readonly static List<string> _invalidNumbersOfCnpj = new()
        {
                "00.000.000/0000-00",
                "11.111.111/1111-11",
                "22.222.222/2222-22",
                "33.333.333/3333-33",
                "44.444.444/4444-44",
                "55.555.555/5555-55",
                "66.666.666/6666-66",
                "77.777.777/7777-77",
                "88.888.888/8888-88",
                "99.999.999/9999-99"
        };

        public static string CheckedCnpj(string cnpj)
        {
            if (_invalidNumbersOfCnpj.Contains(cnpj) || string.IsNullOrEmpty(cnpj))
            {
                throw new CpfException("Invalid cnpj.");
            }
            CheckDigit(TransformCnpj(cnpj));

            return Regex.Replace(cnpj, @"[A-Za-z-\W]+", "");
        }
        private static void CheckDigit(int[] cnpj)
        {
            for (int l = 2; l > 0; l--)
            {
                int result = 0;
                int initialMultiplier = 2;
                for (int i = l; i < cnpj.Length; i++)
                {
                    if (initialMultiplier > 9)
                        initialMultiplier = 2;

                    result += cnpj[i] * initialMultiplier;
                    initialMultiplier++;
                }

                int digit = (result % 11) < 2 ? 0 : 11 - (result % 11);
                if (digit == cnpj[l - 1])
                {
                    continue;
                }
                else
                {
                    throw new CpfException("Invalid cnpj.");
                }
            }
        }
        private static int[] TransformCnpj(string cnpj)
        {
            string pattern = @"[A-Za-z-\W]+";

            char[] charOfCnpj = Regex.Replace(cnpj, pattern, "").ToArray();
            int[] invertedCnpj = new int[14];

            if (charOfCnpj.Length > 14 || charOfCnpj.Length < 14)
            {
                throw new CnpjException("Invalid cnpj.");
            }
            for (int i = 0; i < charOfCnpj.Length; i++)
            {
                int position = (charOfCnpj.Length - 1) - i;

                invertedCnpj[i] = int.Parse(charOfCnpj[position].ToString());
            }
            return invertedCnpj;
        }
    }
}
