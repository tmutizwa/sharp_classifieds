using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Bug
    {
        public int BugId { get; set; }
        public string Detail { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Updated { get; set; }
    }
}