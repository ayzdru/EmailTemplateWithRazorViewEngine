using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTemplateWithRazorViewEngine.ViewModels
{
    public class ThankYouEmailTemplateViewModel
    {
        public ThankYouEmailTemplateViewModel(string fullName, string description)
        {
            FullName = fullName;
            Description = description;
        }

        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
