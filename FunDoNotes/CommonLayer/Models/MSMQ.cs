using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

namespace CommonLayer.Models
{
    public class MSMQ
    {
        MessageQueue messageQueue = new MessageQueue();
        public void SendData2Queue(string token)
        {
            messageQueue.Path = @".\Private$\Token";
            try
            {
                if (MessageQueue.Exists(messageQueue.Path))
                {
                }
                else
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }
           private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
           {
                try
                {
                    var msg = messageQueue.EndReceive(e.AsyncResult);
                    string token = msg.Body.ToString();
                   MailMessage mailMessage = new MailMessage();
                   SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                   {
                     Port=587,
                     EnableSsl = true,
                     Credentials=new NetworkCredential("rajeshwarigandi26@gmail.com","yashraj2426")
                   };
                   mailMessage.From=new MailAddress("rajeshwarigandi26@gmail.com");
                   mailMessage.To.Add(new MailAddress("rajeshwarigandi26@gmail.com"));
                   mailMessage.Body = token;
                   mailMessage.Subject = "FunDoReset Link";
                   smtpClient.Send(mailMessage);
                   messageQueue.BeginReceive();
                }
                catch (MessageQueueException qexception)
                { 
                    throw qexception;
                }
            }
        
    }
}
