using CadastroDeClientes.Domain.Enums;
using CadastroDeClientes.Domain.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Domain.Entities
{
    public class Cliente
    {
        public int? ClienteId { get; set; }
        [Required(ErrorMessage ="Informe o CPF do cliente")]
        public string Cpf { get; set; }
        public const int NomeMaxValue = 150;
        public const int NomeMinValue = 4;
        [StringLength(160, MinimumLength =4, ErrorMessage ="Mínimo de 4 e máximo de 160 caracteres")]
        [Required(ErrorMessage ="Informe o nome do cliente")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Informe uma data válida")]
        [Display(Name ="Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [Display(Name ="Sexo")]
        public Genero Sexo { get; set; }

        public Cliente()
        {

        }

        public Cliente(string cpf, string nome, DateTime dataNascimento, Genero sexo)
        {
            SetCpf(cpf);
            SetNome(nome);
            SetDataNascimento(dataNascimento);
            SetSexo(sexo);

        }

        public void SetCpf(string cpf)
        {
            if (cpf == null)
                throw new Exception("Favor informar o Cpf");

            Cpf = cpf;
        }


        public void SetNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new Exception("Favor informar o Nome");

            int length = nome.Length;

            if (length > NomeMaxValue)
                throw new Exception("Limite máximo de " + NomeMaxValue + " caracteres.");

            if (length < NomeMinValue)
                throw new Exception("Limite mínimo de " + NomeMinValue + " caracteres.");

            Nome = nome;
        }

        public void SetDataNascimento(DateTime dataNascimento)
        {
            DateTime Temp;

            if (DateTime.TryParse(dataNascimento.ToString(), out Temp) == false)
                throw new Exception("Favor informar uma data e nascimento válida.");

            if (!Helper.GetDataNascimentoValida(dataNascimento))
                throw new Exception("Não é permitido o cadastro de cliente menor de idade.");


            DataNascimento = dataNascimento;

        }

        public void SetSexo(Genero sexo)
        {
            if (!(Enum.IsDefined(typeof(Genero), sexo)))
                throw new Exception("Favor informar o Sexo.");
            Sexo = sexo;
        }
    }
}
