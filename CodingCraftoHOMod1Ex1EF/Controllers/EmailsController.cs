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
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EnviarEmail([Bind(Include = "ClienteId, Mensagem")]EnvioDeEmailModel email)
        {
            Cliente cliente = await db.Clientes.FindAsync(email.ClienteId);

            var listaDeProdutosConsumidos = from c in db.Clientes
                                            join v in db.Vendas on c.ClienteId equals v.ClienteId
                                            join i in db.Itens on v.VendaId equals i.VendaId
                                            where v.DataDaVenda.Month == DateTime.Now.Month
                                            select new
                                            {
                                                nome = i.NomeItem,
                                                preco = i.PrecoUnitario,
                                                quantidade = i.Quantidade,
                                            };

            var smtpClient = new SmtpClient
            {
                Host = "smtp-mail.outlook.com", // SMTP
                Port = 587, // Porta
                EnableSsl = true,
                // login //
                Credentials = new System.Net.NetworkCredential("diegol.aquino@outlook.com", "senha")
            };

            using (var message = new MailMessage("diegol.aquino@outlook.com", cliente.Email)
            {
                Subject = "Lembrete de Pagamento",
                Body = "Olá, segue o total a lista das suas compras no mês: "
            })
            {
                int cont = 1;
                string listaProdutosEmail = "\r\n";

                foreach(var item in listaDeProdutosConsumidos)
                {
                    listaProdutosEmail += cont.ToString() + "º - " + item.nome + " - Quantidade: " + item.quantidade.ToString() + " - Preço: R$ " + item.preco.ToString() + "\r\n";
                    cont++;
                }
                
                message.Body += listaProdutosEmail + "\r\n" + "Total no mês: R$ " + listaDeProdutosConsumidos.Sum(l => l.preco).ToString() + "\r\n" + email.Mensagem;

                await smtpClient.SendMailAsync(message);
            }

            return RedirectToAction("Sent");
        }

        public ActionResult Sent() => View();
    }
}