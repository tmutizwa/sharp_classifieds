using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization; 

namespace Classifieds.Areas.Admin.Models
{
    [DataContract]
    public class BulkPrintClassifieds
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string Details { get; set; }
        [DataMember]
        public string author { get; set; }
        [DataMember]
        public string date { get; set; }
    }
}