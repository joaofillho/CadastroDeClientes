using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Persistence.Context;
using System;
using System.Data.Entity;
using System.Linq;

namespace CadastroDeClientes.Persistence.DAL
{
    public class ClienteDAL
    {
        private DbCLientesContexto db = new DbCLientesContexto();


        public IQueryable GetClientesOrderByNome()
        {
            return db.Clientes.OrderBy(l => l.Nome).Select(x => new
                {
                    ClienteId = x.ClienteId,
                    Nome = x.Nome,
                    DataNascimento = x.DataNascimento,
                    Cpf = x.Cpf,
                    Sexo = x.Sexo.ToString()
                });
        }

        public Cliente GetClienteById(long id)
        {
            return db.Clientes.Where(l => l.ClienteId == id).First();
        }

        //Para novos cadastros verifica se o cpf já foi cadastrado
        public void SaveCliente(Cliente cliente)
        {
                if (cliente.ClienteId == null)
                {
                    if (!CpfExiste(cliente))
                    {
                        db.Clientes.Add(cliente);
                    }
                    else
                    {
                        throw new Exception("Cpf já existe na base de dados.");
                    }
                }
                else
                {
                    db.Entry(cliente).State = EntityState.Modified;
                }

        }

        public bool CpfExiste(Cliente cliente)
        {
            Cliente c = db.Clientes.Where(p => p.Cpf == cliente.Cpf).FirstOrDefault();
            return c != null;
        }

        public void DeleteCliente(long id)
        {
            Cliente cliente = GetClienteById(id);
            db.Clientes.Remove(cliente);

            //return cliente;
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
