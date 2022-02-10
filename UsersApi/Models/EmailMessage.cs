using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsersApi.Models
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailMessage(IEnumerable<string> to, string subject, int userId, string activationCode)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(t => new MailboxAddress(t)));
            Subject = subject;
            Body = $"http://localhost:6000/active?UserId={userId}&ActivationCode={activationCode}";
        }
    }
}
