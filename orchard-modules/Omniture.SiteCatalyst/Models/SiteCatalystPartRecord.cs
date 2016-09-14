using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace Omniture.SiteCatalyst.Models
{
    public class SiteCatalystPartRecord: ContentPartRecord
    {
        public virtual string PageName { get; set; }
        public virtual string PageType { get; set; }
    }
}