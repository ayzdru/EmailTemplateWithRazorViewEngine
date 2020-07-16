using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailTemplateWithRazorViewEngine.BindingModels;
using EmailTemplateWithRazorViewEngine.Interfaces;
using EmailTemplateWithRazorViewEngine.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace EmailTemplateWithRazorViewEngine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEmailService _emailService;

        public IndexModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void OnGet()
        {

        }
        [BindProperty]
        public SendEmailBindingModel SendEmailBindingModel { get; set; }
        public void OnPostSendEmail()
        {
            var thankYouEmailTemplateHtml = _emailService.GetPartialViewHtml("../EmailTemplates/ThankYouEmailTemplate", new ThankYouEmailTemplateViewModel("Ayaz Duru", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eu ullamcorper tellus. Nulla eu lacinia metus. Duis tempor dictum tortor a ultricies. Donec interdum neque quis lacus porta, a varius dui iaculis."));
            _emailService.SendEmail(SendEmailBindingModel.Email,"Thank You" , thankYouEmailTemplateHtml);
        }
    }
}
