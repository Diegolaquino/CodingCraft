using CodingCraftoHOMod1Ex1EF.Helper;
using CodingCraftoHOMod1Ex1EF.Models;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class EmailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Emails
        public ActionResult Index()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EnviarEmail([Bind(Include = "Id, Mensagem")]EnvioDeEmailModel email)
        {
            ApplicationUser user = db.Users.Find(email.Id);

            var listaDeProdutosConsumidos = from c in db.Users
                                            join v in db.Vendas on c.Id equals v.UserId
                                            join i in db.Itens on v.VendaId equals i.VendaId
                                            where v.DataDaVenda.Month == DateTime.Now.Month
                                            select new
                                            {
                                                nome = i.NomeItem,
                                                preco = i.PrecoUnitario,
                                                quantidade = i.Quantidade,
                                            };


            string mensagemEmail = "";
            int cont = 1;
            string listaProdutosEmail = "\r\n";

            foreach (var item in listaDeProdutosConsumidos)
            {
                listaProdutosEmail += cont + "º - " + item.nome + " - Quantidade: " + item.quantidade.ToString() + " - Preço: R$ " + item.preco + "\r\n";
                cont++;
            }

            mensagemEmail += listaProdutosEmail + "\r\n" + "Total no mês: R$ " + listaDeProdutosConsumidos.Sum(l => l.preco).ToString() + "\r\n" + email.Mensagem;

            await EmailHelper.EnviarEmailAsync(user.Email, "Lembrete de Pagamento", mensagemEmail);

            return RedirectToAction("Sent");
        }

        public ActionResult Sent() => View();
    }
}