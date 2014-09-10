using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classifieds.Areas.Admin.Models.ViewModels
{
    public class CreateStorefrontViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
    }
}