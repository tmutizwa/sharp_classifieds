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
        public string ContentJson { get; set; }
        [Required]
        public string MapperJson { get; set; }
    }
}