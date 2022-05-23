using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderApp
{
    public class ConnectToEmail
    {
        public string emailOrigin = ConfigurationManager.AppSettings["emailOrigin"];
        public string password = ConfigurationManager.AppSettings["password"];
        public int port = 995;
        public bool useSSL = true;
        public string Hostname = "pop.gmail.com";

        public List<OpenPop.Mime.Message> GetMessages()
        {
            using (Pop3Client oClient = new Pop3Client())
            {
                oClient.Connect(Hostname, port, useSSL);
                oClient.Authenticate(emailOrigin, password);
                int messageCount = oClient.GetMessageCount();
                List<OpenPop.Mime.Message> lstMessages = new List<OpenPop.Mime.Message>(messageCount);
                for (int i = messageCount; i > 0; i--)
                {
                    lstMessages.Add(oClient.GetMessage(i));
                }
                return lstMessages;
            }
        }
    }
}
