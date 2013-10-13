using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.Services
{
    public class ShareServicePhone : IShareService
    {
        public void ShareProjectWithMail(string projectName, string mailTo, string subject, string message)
        {
            var emailcomposer = new EmailComposeTask();
            emailcomposer.To = mailTo;
            emailcomposer.Subject = subject;
            emailcomposer.Body = message;
            emailcomposer.Show();
        }
    }
}
