using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Omniture.SiteCatalyst.Models
{
    public class SiteCatalystPart : ContentPart<SiteCatalystPartRecord> {
        public string PageName {
            get { return Record.PageName; }
            set { Record.PageName = value; }
        }

        public string PageType {
            get { return Record.PageType; }
            set { Record.PageType = value; }
        }
    }
}