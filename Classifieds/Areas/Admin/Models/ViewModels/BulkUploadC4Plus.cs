using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Areas.Admin.Models.ViewModels
{
    public class BulkUploadC4Plus
    {
        public BulkUploadC4Plus() { }
        [Required]
        public string Text { get; set; }
    }
}