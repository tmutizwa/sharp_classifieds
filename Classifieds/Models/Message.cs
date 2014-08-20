using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Type { get; set; }
        public string SenderName { get; set; }
        public string SenderId { get; set; }
        public int ListingId { get; set; }
        public string Detail { get; set; }
        public string SenderEmail { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Updated { get; set; }
    }
}