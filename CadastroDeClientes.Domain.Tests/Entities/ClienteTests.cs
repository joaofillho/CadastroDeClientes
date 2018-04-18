using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CadastroDeClientes.Domain.Tests.Entities
{
    [TestClass]
    public class ClienteTests
    {
        private string Cpf { get; set; }
        private string Nome { get; set; }
        private DateTime DataNascimento { get; set; }
        private Genero Sexo { get; set; }
        private Cliente Cliente { get; set; }

        public ClienteTests()
        {
            Cpf = "41721604251";
            Nome = "Maria da Silva Sauro";
            DataNascimento = new DateTime(1970, 1, 1);
            Sexo = Genero.F;

            Cliente = new Cliente(Cpf, Nome, DataNascimento, Sexo);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Cliente_New_CPF_Obrigatorio()
        {
            new Cliente(null, Nome, DataNascimento, Sexo);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Cliente_New_Nome_Obrigatorio()
        {
            new Cliente(Cpf, "", DataNascimento, Sexo);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Cliente_New_DataNascimento_Obrigatorio()
        {
            new Cliente(Cpf, Nome, new DateTime(99, 999, 99), Sexo);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Cliente_New_Sexo_Obrigatorio()
        {
            new Cliente(Cpf, Nome, DataNascimento, (Genero)5);
        }

        [TestMethod]
        public void Cliente_New_Cpf()
        {
            var cliente = new Cliente(Cpf, Nome, DataNascimento, Sexo);
            Assert.AreEqual(Cpf, cliente.Cpf);
        }

        [TestMethod]
        public void Cliente_New_Nome()
        {
            var cliente = new Cliente(Cpf, Nome, DataNascimento, Sexo);
            Assert.AreEqual(Nome, cliente.Nome);
        }

        [TestMethod]
        public void Cliente_New_DataNascimento()
        {
            var cliente = new Cliente(Cpf, Nome, DataNascimento, Sexo);
            Assert.AreEqual(DataNascimento, cliente.DataNascimento);
        }

        [TestMethod]
        public void Cliente_New_Sexo()
        {
            var cliente = new Cliente(Cpf, Nome, DataNascimento, Sexo);
            Assert.AreEqual(Sexo, cliente.Sexo);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Cliente_SetNome_Min_Value()
        {
            Cliente.SetNome("jo");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Cliente_SetNome_Max_Value()
        {
            
            Cliente.SetNome("ipriano Serafim de Bragança Pedro de Alcântara Francisco Antônio João Carlos Xavier de Paula Miguel Rafael Joaquim José Gonzaga Pascoal C e Bourbon etc e tal");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Cliente_SetDataNascimento_Invalida()
        {
            DateTime data = new DateTime(2015, 01, 25);
            Cliente.SetDataNascimento(data);
        }

    }
}
