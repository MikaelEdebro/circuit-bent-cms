using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace CircuitBentCMS.Models
{
    public class MessageService
    {

        // annonsbekräftelse skickas efter man lagt in en annons. Innehåller lösenord till att redigera.
        public static string SendMail(string email, string subject, string message)
        {
            string error = "";
            var context = new CircuitBentCMSContext();
            var credentials = context.MailSettings.FirstOrDefault();

            try
            {
                MailMessage mm = new MailMessage(email, credentials.Email);
                mm.Subject = subject;
                mm.Body = message;

                // set encoding and html or text
                mm.BodyEncoding = Encoding.GetEncoding("utf-8");
                mm.IsBodyHtml = false;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mm);
 
            }
            catch (Exception e) {
                error = e.Message;
            }

            return error;
        }
    }
}