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
			// Gmail account email address and password
			string fromEmail = _DotEnv.Get("fromEmail");
			string password = _DotEnv.Get("password");

			// Creating the MailMessage object
			MailMessage message = new MailMessage(fromEmail, recipient, subject, body);
			message.IsBodyHtml = true;

			// Configuring the SMTP client for Gmail
			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
			smtpClient.EnableSsl = true;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(fromEmail, password);

			try
			{
				// send email
				smtpClient.Send(message);
			}
			catch (Exception ex)
			{
			}
			finally
			{
				// Resource release
				message.Dispose();
				smtpClient.Dispose();
			}
		}
	}
}