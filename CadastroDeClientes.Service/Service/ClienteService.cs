using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Persistence.DAL;
using System.Linq;
namespace CadastroDeClientes.Service.Service
{
    public class ClienteService
    {
        private ClienteDAL db = new ClienteDAL();

        public IQueryable GetClientesOrderByNome()
        {
            return db.GetClientesOrderByNome();
        }

        public Cliente GetClienteById(long id)
        {
            return db.GetClienteById(id);
        }

        public void SaveCliente(Cliente cliente)
        {
            db.SaveCliente(cliente);
        }

        public void DeleteCliente(long id)
        {
            db.DeleteCliente(id);
        }

        public void Commit()
        {
            db.Commit();
        }

        public void Dispose()
        {
            db.Dispose();

        }
    }
}
