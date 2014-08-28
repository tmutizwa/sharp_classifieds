using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace Classifieds.Library
{
    public static class TMSendEmail
    {
        public static void send(string from, string fromName, string to, string subject, string body, string copyEmail)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            if (!String.IsNullOrEmpty(copyEmail))
                mail.CC.Add(copyEmail);
            mail.From = new MailAddress(from,fromName);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            smtp.Host = "SVRMAIL.zimpapers.co.zw";
            //smtp.Port = 587;
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.Credentials = new System.Net.NetworkCredential("info@infinisys.co.zw", "oneTwo3!");
            smtp.Credentials = new System.Net.NetworkCredential("achinyangare", "Pass20word14#");
            //smtp.EnableSsl = true;
            smtp.EnableSsl = false;
            try
            {
                smtp.Send(mail);
            }catch(Exception e){
                throw new Exception("Error sending email.");
            }
            

            //WebMail.SmtpServer = "SVRMAIL.zimpapers.co.zw";
            //WebMail.SmtpPort = 25;
            //WebMail.EnableSsl = false;
            //WebMail.UserName = "achinyangare";
            //WebMail.Password = "Pass20word14#";
            //WebMail.From = from;
            //WebMail.Send(to,subject,body,null,copyEmail);
          
        }
    }
}