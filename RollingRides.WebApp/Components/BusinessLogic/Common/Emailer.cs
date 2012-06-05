using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
namespace RollingRides.WebApp.Components.BusinessLogic.Common
{
    public class Emailer
    {
        private readonly string _host, _username, _password;
        private readonly int? _port;
        public Emailer(string host, string username, string password, int? port = null)
        {
            _host = host;
            _username = username;
            _password = password;
            _port = port;
        }
        
        public void SendHtmlEmail(string emailFrom, string emailTo, string subject, string body)
        {
        
            var smtpClient = _port == null ? new SmtpClient(_host) : new SmtpClient(_host, _port.Value);
            var mailMessage = new MailMessage(emailFrom, emailTo);
            if (!body.ToLower().StartsWith("<html>"))
                body = "<html>" + body;
            if (!body.ToLower().Contains("<body>"))
                body = body.Insert(body.IndexOf("<html>") + 5, "<body>");
            if (!body.ToLower().EndsWith("</body></html>"))
                body = body + "</body></html>";

            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            smtpClient.Send(mailMessage);
        }
    }
}