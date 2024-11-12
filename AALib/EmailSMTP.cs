using System.Net.Mail;
using System.Net;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Xml.Linq;

namespace AALib
{
    public class EmailSMTP
    {
        string smtpServerAddress = "smtp.gmail.com";
        int smtpServerPort = 587;
        string smtpServerUserId = "aa8554495610@gmail.com";
        string smtpServerPassword = "twnqcmbipdqiigkm";
        string ToEmailIds = "nitin@gnxtsystems.com;aa8554495610@gmail.com";
        string emailFromAddress = "aa8554495610@gmail.com";
        string displayName = "Support@aa.com";


        public void SendEmail(string receiverEmail, string subjuect, string body)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(emailFromAddress, displayName)
            };

            mail.To.Add(receiverEmail);
            mail.Subject = subjuect;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient(smtpServerAddress, smtpServerPort))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smtpServerUserId, smtpServerPassword);
                try
                {
                    smtp.Send(mail);
                    Console.WriteLine($"\tEmail notification process finished at {DateTime.UtcNow}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }
        public void SendSupportEmail(string receiverEmail, string subjuect, string body, string fileName, string fileType, string fileBytes)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(emailFromAddress, displayName)
            };

            mail.To.Add(receiverEmail);
            mail.Subject = subjuect;
            mail.Body = body;
            mail.IsBodyHtml = true;
            if (fileName.Length > 0)
            {
                // convert string to stream
                byte[] byteArray = Convert.FromBase64String(fileBytes);
                MemoryStream stream = new MemoryStream(byteArray);

                Attachment attchment = new Attachment(stream, fileName, fileType);
                //stream.Close();

                mail.Attachments.Add(attchment);
            }

            using (SmtpClient smtp = new SmtpClient(smtpServerAddress, smtpServerPort))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smtpServerUserId, smtpServerPassword);
                try
                {
                    smtp.Send(mail);
                    Console.WriteLine($"\tEmail notification process finished at {DateTime.UtcNow}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }
    }
}
