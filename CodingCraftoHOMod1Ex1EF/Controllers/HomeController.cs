using CodingCraftoHOMod1Ex1EF.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Hangfire;
using System.Threading.Tasks;
using System.Net.Mail;
using CodingCraftoHOMod1Ex1EF.Models.Enums;
using System.Data.Entity;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            BackgroundJob.Enqueue(() => LembretePagamentoFornecedor());
            return View();
        }

        public async Task LembretePagamentoFornecedor()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DateTime currentDate = DateTime.Now;
            var eventos = await db.Eventos.Where(e => e.TipoDeEvento == (int)TipoDeEvento.PagamentoFornecedor && DbFunctions.TruncateTime(e.DataDeAviso) == currentDate.Date).ToListAsync();

            if(eventos.Any())
            {
                var smtpClient = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com", // SMTP
                    Port = 587, // Porta
                    EnableSsl = true,
                    // login //
                    Credentials = new System.Net.NetworkCredential("diegol.aquino@outlook.com", "senha")
                };

                using (var message = new MailMessage("diegol.aquino@outlook.com", "diegol.aquino@gmail.com")
                {
                    Subject = "Lembrete de Pagamento",
                    Body = "Esses são os fornecedores que você deve pagar: "
                })
                {
                    int cont = 1;
                    string m = "\r\n";

                    foreach (Evento evento in eventos)
                    {
                        m += cont.ToString() + " " + evento.Aviso + "\r\n";
                        cont++;
                    }

                    message.Body += m;

                    await smtpClient.SendMailAsync(message);
                }
            }
        }
    }
}