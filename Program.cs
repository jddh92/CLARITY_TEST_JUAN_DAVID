using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace EmailSenderApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Sender sendEmail = new Sender();
            sendEmail.SenderEmail();
            Console.WriteLine("Press any key to close the window");
            Console.ReadLine();
        }
    }    
}
