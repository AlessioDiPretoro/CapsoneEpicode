using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace Stones.Models
{
    public class _Mailer
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Indirizzo email e password dell'account Gmail
            string fromEmail = "alessiodipretoro@gmail.com";
            string password = "qmik iohu gfyk bjno";

            // Creazione dell'oggetto MailMessage
            MailMessage message = new MailMessage(fromEmail, recipient, subject, body);
            message.IsBodyHtml = true;

            // Configurazione del client SMTP per Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromEmail, password);

            try
            {
                // Invio dell'email
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                // Gestione degli errori o eccezioni
                // Puoi loggare l'errore o gestirlo come preferisci
            }
            finally
            {
                // Rilascio delle risorse
                message.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}