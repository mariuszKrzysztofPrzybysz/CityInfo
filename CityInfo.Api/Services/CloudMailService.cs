using CityInfo.Api.Interfaces;
using System.Diagnostics;

namespace CityInfo.Api.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private readonly string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            //Send email - output to debug window
            Debug.WriteLine(string.Empty);
            Debug.Indent();
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(CloudMailService)}");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
            Debug.Unindent();
            Debug.WriteLine(string.Empty);
        }
    }
}