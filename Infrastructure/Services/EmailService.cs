using Application.IServices;
using Domain.Models;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SecondHandDbContext _db;

        public EmailService(SecondHandDbContext secondHandDbContext) { 
            _db = secondHandDbContext;
        }
        public async Task SendForgotPassword(string recipientEmail)
        {
            //create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_db.GetSenderEmail()));
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = "Forgot Password Hand2Hand Account";
            email.Body = new TextPart(TextFormat.Html) { Text = "Hello Here your token" };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            string emailSender = _db.GetSenderEmail();
            string password = _db.GetSenderPassword();
            smtp.Authenticate(_db.GetSenderEmail(), _db.GetSenderPassword());
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}