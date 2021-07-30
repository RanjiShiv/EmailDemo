using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {

                EnableSsl = false,
                /*send email using localhost
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
                */
                //Store emails in a local folder
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //Create the folder if it does not exist
                PickupDirectoryLocation = @"C:\Emails"
            });

            //SendEmailUsingTemplateString(sender);
            SendEmailUsingTemplateFile(sender);



        }

        private static async void SendEmailUsingTemplateString( SmtpSender sender)
        {
            StringBuilder template = new StringBuilder();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thanks for purchasing @Model.ProductName. We hope you enjoy it.</p>");
            template.AppendLine("- The TimCo Team");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("tim@timco.com")
                .To("typicalmilkyway@gmail.com", "Ranjitha Shivarudraiah")
                .Subject("From email demo App!")
                .UsingTemplate(template.ToString(), new { FirstName = "Ranjitha", ProductName = "Bacon-Wrapped Bacon" })
                // .Body("This email is sent from the email demo app.")
                .SendAsync();
        }


        private static async void SendEmailUsingTemplateFile(SmtpSender sender)
        {
            StringBuilder template = new StringBuilder();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thanks for purchasing @Model.ProductName. We hope you enjoy it.</p>");
            template.AppendLine("- The TimCo Team");
            //set some email properties
            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            //figure out a way to set the model in razor pages (currently using inline models)


            Console.Write(Directory.GetCurrentDirectory());


            var email = await Email
                .From("tim@timco.com")
                .To("typicalmilkyway@gmail.com", "Ranjitha Shivarudraiah")
                .Subject("From email demo App!")
                .UsingTemplateFromFile($"{Path.Combine("../../../", "Templates\\sample.cshtml")}",
                new { FirstName = "Ranjitha", ProductName = "Bacon-Wrapped Bacon" })
                // .Body("This email is sent from the email demo app.")
                .SendAsync();
        }


    }
}
