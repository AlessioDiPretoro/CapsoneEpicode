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
            _DotEnv.Load();
            // Indirizzo email e password dell'account Gmail
            string fromEmail = _DotEnv.Get("fromEmail");
            string password = _DotEnv.Get("password");

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