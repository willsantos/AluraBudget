
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class EmailService
    {
        private IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string[] to, string subject, int userId, string activationCode)
        {
            EmailMessage message = new EmailMessage(to, subject, userId, activationCode);
            var messageToEmail = SetEmailBody(message);
            Send(messageToEmail);
        }

        private void Send(MimeMessage messageToEmail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_config.GetValue<string>("EmailSettings:SmtpServer"),
                        _config.GetValue<int>("EmailSettings:Port"),
                        false
                        );
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(
                        _config.GetValue<string>("EmailSettings:From"),
                        _config.GetValue<string>("EmailSettings:Password")
                        );
                    client.Send(messageToEmail);
                }   
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage SetEmailBody(EmailMessage message)
        {
           var messageToEmail = new MimeMessage();
            messageToEmail.From.Add(new MailboxAddress(_config.GetValue<string>("EmailSettings:From")));
            messageToEmail.To.AddRange(message.To);
            messageToEmail.Subject = message.Subject;
            messageToEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Body
            };

            return messageToEmail;
        }
    }
}
