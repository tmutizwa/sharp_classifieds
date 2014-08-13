using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Classifieds.Library
{
    public static class TMSendEmail
    {
        public static void send(string from, string to , string subject, string body,string copyEmail)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            if (!String.IsNullOrEmpty(copyEmail))
                mail.CC.Add(copyEmail);
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.mandrillapp.com";
            smtp.Port = 2525;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("terry@infinisys.co.zw", "KSgsi3c5ZOsrgp82AOT6HA");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}