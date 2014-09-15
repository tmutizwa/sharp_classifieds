using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization; 

namespace Classifieds.Areas.Admin.Models
{
    [DataContract]
    public class BulkPrintClassifiedsMapper
    {
        //the category title provided from print bulk uploader
        [DataMember]
        public string print_category { get; set; }
        [DataMember]
        //the category id we are targeting
        public int web_category_id { get; set; }
    }
}