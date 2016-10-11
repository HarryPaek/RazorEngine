using Common.Data.Models;
using Common.Data.ViewModels;
using ConsoleApplication.Models;
using ConsoleApplication.ViewModel;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static int Main(string[] args)
        {
            // int exitCode = SimpleWelcomeTemplate();
            // int exitCode = WelcomeWithViewModelTemplate();
            int exitCode = WelcomeWithLinkedViewModelTemplate();

            return exitCode;
        }

        private static int SimpleWelcomeTemplate()
        {
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                // RazorEngine cannot clean up from the default appdomain...
                Console.WriteLine("Switching to secound AppDomain, for RazorEngine...");
                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var current = AppDomain.CurrentDomain;
                // You only need to add strongnames when your appdomain is not a full trust environment.
                var strongNames = new StrongName[0];

                var razorEngineDomain = AppDomain.CreateDomain("MyRazorEngineDomain", null, current.SetupInformation, new PermissionSet(PermissionState.Unrestricted), strongNames);
                var exitCode = razorEngineDomain.ExecuteAssembly(Assembly.GetExecutingAssembly().Location);

                // RazorEngine will cleanup. 
                AppDomain.Unload(razorEngineDomain);
                return exitCode;
            }

            var templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");
            var templateFilePath = Path.Combine(templateFolderPath, "WelcomeEmail.cshtml");

            // Create a model for our email
            var model = new UserModel() { Name = "Sarah", Email = "sarah@mail.example", IsPremiumUser = false };

            //  Generate the email body from the template file.
            // 'templateFilePath' should contain the absolute path of your template file.
            var emailHtmlBody = Engine.Razor.RunCompile(File.ReadAllText(templateFilePath), "templateKey", typeof(UserModel), model);

            // Send the email
            var email = new MailMessage()
            {
                Body = emailHtmlBody,
                IsBodyHtml = true,
                Subject = "Welcome"
            };

            email.To.Add(new MailAddress(model.Email, model.Name));
            // The From field will be populated from the app.config value by default

            var smtpClient = new SmtpClient();
            smtpClient.Send(email);

            Console.ReadLine();

            return 0;
        }

        private static int WelcomeWithViewModelTemplate()
        {
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                // RazorEngine cannot clean up from the default appdomain...
                Console.WriteLine("Switching to secound AppDomain, for RazorEngine...");
                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var current = AppDomain.CurrentDomain;
                // You only need to add strongnames when your appdomain is not a full trust environment.
                var strongNames = new StrongName[0];

                var razorEngineDomain = AppDomain.CreateDomain("MyViewModelRazorEngineDomain", null, current.SetupInformation, new PermissionSet(PermissionState.Unrestricted), strongNames);
                var exitCode = razorEngineDomain.ExecuteAssembly(Assembly.GetExecutingAssembly().Location);

                // RazorEngine will cleanup. 
                AppDomain.Unload(razorEngineDomain);
                return exitCode;
            }

            var templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");
            var templateFilePath = Path.Combine(templateFolderPath, "WelcomeWithViewModel.cshtml");

            // Create a model for our email
            var model = new UserListViewModel() { Receiver = new UserModel() { Name = "Sarah", Email = "sarah@example.com", IsPremiumUser = false }};
            model.RelatedUsers.Add(new UserModel() { Name = "Harry", Email = "harry@example.com", IsPremiumUser = false });
            model.RelatedUsers.Add(new UserModel() { Name = "Bob", Email = "bob@example.com", IsPremiumUser = true });
            model.RelatedUsers.Add(new UserModel() { Name = "Jane", Email = "jane@example.com", IsPremiumUser = false });

            //  Generate the email body from the template file.
            // 'templateFilePath' should contain the absolute path of your template file.
            var emailHtmlBody = Engine.Razor.RunCompile(File.ReadAllText(templateFilePath), "viewModelTemplateKey", typeof(UserListViewModel), model);

            // Send the email
            var email = new MailMessage()
            {
                Body = emailHtmlBody,
                IsBodyHtml = true,
                Subject = "Welcome with ViewModel"
            };

            email.To.Add(new MailAddress(model.Receiver.Email, model.Receiver.Name));
            // The From field will be populated from the app.config value by default

            var smtpClient = new SmtpClient();
            smtpClient.Send(email);

            Console.ReadLine();

            return 0;
        }

        private static int WelcomeWithLinkedViewModelTemplate()
        {
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                // RazorEngine cannot clean up from the default appdomain...
                Console.WriteLine("Switching to secound AppDomain, for RazorEngine...");
                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var current = AppDomain.CurrentDomain;
                // You only need to add strongnames when your appdomain is not a full trust environment.
                var strongNames = new StrongName[0];

                var razorEngineDomain = AppDomain.CreateDomain("MyLinkedViewModelRazorEngineDomain", null, current.SetupInformation, new PermissionSet(PermissionState.Unrestricted), strongNames);
                var exitCode = razorEngineDomain.ExecuteAssembly(Assembly.GetExecutingAssembly().Location);

                // RazorEngine will cleanup. 
                AppDomain.Unload(razorEngineDomain);
                return exitCode;
            }

            var templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");
            var templateFilePath = Path.Combine(templateFolderPath, "WelcomeWithLinkedViewModel.cshtml");

            // Create a model for our email
            var sarah = new LinkedUserModel() { Name = "Sarah", Email = "sarah@example.com", IsPremiumUser = false };
            var harry = new LinkedUserModel() { Name = "Harry", Email = "harry@example.com", IsPremiumUser = false };
            var bob = new LinkedUserModel() { Name = "Bob", Email = "bob@example.com", IsPremiumUser = true };
            var jane = new LinkedUserModel() { Name = "Jane", Email = "jane@example.com", IsPremiumUser = false };

            sarah.LinkedUsers.Add(harry);
            sarah.LinkedUsers.Add(bob);
            sarah.LinkedUsers.Add(jane);

            harry.LinkedUsers.Add(sarah);
            harry.LinkedUsers.Add(bob);

            bob.LinkedUsers.Add(sarah);
            bob.LinkedUsers.Add(jane);

            jane.LinkedUsers.Add(sarah);
            jane.LinkedUsers.Add(harry);
            

            var model = new LinkedUserListViewModel() { Receiver = sarah };
            model.RelatedUsers.Add(harry);
            model.RelatedUsers.Add(bob);
            model.RelatedUsers.Add(jane);

            //  Generate the email body from the template file.
            // 'templateFilePath' should contain the absolute path of your template file.
            var emailHtmlBody = Engine.Razor.RunCompile(File.ReadAllText(templateFilePath), "linkedViewModelTemplateKey", typeof(LinkedUserListViewModel), model);

            // Send the email
            var email = new MailMessage()
            {
                Body = emailHtmlBody,
                IsBodyHtml = true,
                Subject = "Welcome with Linked ViewModel"
            };

            email.To.Add(new MailAddress(model.Receiver.Email, model.Receiver.Name));
            // The From field will be populated from the app.config value by default

            var smtpClient = new SmtpClient();
            smtpClient.Send(email);

            Console.ReadLine();

            return 0;
        }
    }
}
