using Orchard.ContentManagement.Records;
using System.Collections.Generic;

namespace Northern.News.Models {
    public class ArticlePartRecord : ContentPartRecord {
        public virtual string PrimaryImage { get; set; }
        public virtual string ThumbImage { get; set; }
        public virtual int ArticleID { get; set; }
        public virtual bool IsFeatured { get; set; }
    }
}