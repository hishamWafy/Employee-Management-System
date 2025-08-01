
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Net.Mail;
using Manage.DAL.Entities;

namespace Demo.PL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Manage.DAL.Entities.Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("demustafa74@gmail.com", "hevhyhgtenmwjufo");
            client.Send("demustafa74@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}