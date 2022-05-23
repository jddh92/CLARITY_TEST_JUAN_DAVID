using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSenderApp
{
    public class Sender
    {
        public void SenderEmail()
        {
            var emailOrigin = ConfigurationManager.AppSettings["emailOrigin"];
            var emailRecipient = ConfigurationManager.AppSettings["emailRecipient"];
            var message = ConfigurationManager.AppSettings["message"];
            var body = ConfigurationManager.AppSettings["body"];
            var password = ConfigurationManager.AppSettings["password"];
            var host = ConfigurationManager.AppSettings["Host"];

            MailMessage oMailMesagge = new MailMessage(emailOrigin, emailRecipient, message, body);
            SmtpClient smtpClient = new SmtpClient(host);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = host;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(emailOrigin, password);

            try
            {
                int contador = 0;
                bool flag = false;
                do
                {
                    smtpClient.Send(oMailMesagge);
                    int mydelay = 10000;
                    Thread.Sleep(mydelay);
                    Console.WriteLine("Sending....");
                    ConnectToEmail oConnectToEmail = new ConnectToEmail();
                    List<OpenPop.Mime.Message> emails = oConnectToEmail.GetMessages();
                    foreach (var email in emails)
                    {
                        if (email.Headers.Subject == "Delivery Status Notification (Failure)")
                        {
                            contador++;
                            flag = false;
                            if (contador == 3)
                            {
                                Console.WriteLine("Unable to send the email at the third attemp. Please verify the  recipient email address");
                                flag = true;
                            }
                        }
                        if (email.Headers.Subject != "Delivery Status Notification (Failure)")
                        {
                            flag = false;
                        }
                    }
                    if (emails.Count.Equals(0))
                    {
                        Console.WriteLine("Successfully sent");
                        flag = true;
                    }

                } while (!flag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }        
    }
}
