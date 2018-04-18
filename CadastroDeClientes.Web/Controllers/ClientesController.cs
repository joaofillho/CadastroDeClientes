using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Service.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;


namespace CadastroDeClientes.Web.Controllers
{
    public class ClientesController : Controller
    {
        private ClienteService db = new ClienteService();


        //Lista de clientes paginada 
        public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5)
        {
            var clientes = db.GetClientesOrderByNome();

            var total = clientes.Count();
            var chave = Request.Form.AllKeys.Where(k => k.StartsWith("sort")).First();
            var ordenacao = Request[chave];
            var campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var ordenarPor = string.Format("{0} {1}", campo, ordenacao);


            if (searchPhrase != null)
            {
                clientes = clientes.Where("cpf.Contains(@0) OR Nome.Contains(@0)", searchPhrase);

            }

            var clientesPaginados = clientes.OrderBy(ordenarPor).Skip((current - 1) * rowCount).Take(rowCount);



            return Json(new
            {
                current = current,
                rowCount = rowCount,
                total = total,
                rows = clientesPaginados
            }, JsonRequestBehavior.AllowGet);
        }

        private JsonResult AcaoRetorno(bool retorno, string mensagem)
        {
            return Json(new { retorno, mensagem }, JsonRequestBehavior.AllowGet);
        }

        private JsonResult GravarItem(Cliente cliente)
        {
            if (ModelState.IsValid)
            {

               try
                {
                    db.SaveCliente(cliente);
                    db.Commit();
                    return AcaoRetorno(true, "Cliente gravado com sucesso.");
                }
                catch (Exception e)
                {
                    return AcaoRetorno(false, e.Message);
                }
            }

            IEnumerable<ModelError> erros = ModelState.Values.SelectMany(x => x.Errors);
            return AcaoRetorno(false, "Não foi possível gravar o item atual: " + erros);


        }

        private ActionResult CarregarItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.GetClienteById((int)id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            return PartialView(cliente);
        }

        
        public ActionResult Index()
        {
            return View();
        }

                
        public ActionResult Details(int? id)
        {
            return CarregarItem(id);
        }

                
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            return GravarItem(cliente);
        }

        public ActionResult Edit(int? id)
        {
            return CarregarItem(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            return GravarItem(cliente);
        }

        public ActionResult Delete(int? id)
        {
            return CarregarItem(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                db.DeleteCliente(id);
                db.Commit();
                return AcaoRetorno(true, "Cadastro do cliente excluído com sucesso.");

            }
            catch (Exception e)
            {
                return AcaoRetorno(false, "Ocorreu um erro ao excluir o cadastro do cliente:" + e.Message);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
