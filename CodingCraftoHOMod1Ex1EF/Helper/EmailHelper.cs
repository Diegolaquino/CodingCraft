using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CodingCraftoHOMod1Ex1EF.Helper
{
    public static class EmailHelper
    {
        public static async Task EnviarEmailAsync(string email, string assunto, string mensagem)
        {

            var word = ConfigurationManager.AppSettings["wordpass"];

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp-mail.outlook.com", // SMTP
                Port = 587, // Porta
                EnableSsl = true,
                // login //
                Credentials = new System.Net.NetworkCredential("diegol.aquino@outlook.com", word)
            };

            using (MailMessage message = new MailMessage("diegol.aquino@outlook.com", email)
            {
                Subject = assunto,
            })
            {
                message.Body = mensagem;

                await smtpClient.SendMailAsync(message);
            }

        }

        
    }
}