using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTemplateWithRazorViewEngine.Interfaces
{
    public interface IEmailService
    {
        string GetPartialViewHtml(string partialViewName, object model);
        void SendEmail(string email, string subject, string body);

    }
}
