using System;
using System.Linq;

namespace CadastroDeClientes.Helpers
{
    public class Helper
    {
        public static string GetNumeros(string value)
        {
            return string.IsNullOrEmpty(value) ? "" : new String(value.Where(Char.IsDigit).ToArray());

        }

        public static bool GetDataNascimentoValida(DateTime DataNascimento)
        {

            int idade = DateTime.Now.Year - DataNascimento.Year;
            if (DateTime.Now.Month < DataNascimento.Month || (DateTime.Now.Month == DataNascimento.Month && DateTime.Now.Day < DataNascimento.Day))
                idade--;

            return idade > 18;
        }

    }
}