using Orchard.ContentManagement;

namespace Northern.News.Models {
    public class ArticlePart : ContentPart<ArticlePartRecord> {
        public string PrimaryImage
        {
            get { return Record.PrimaryImage; }
            set { Record.PrimaryImage = value; }
        }
        public string ThumbImage
        {
            get { return Record.ThumbImage; }
            set { Record.ThumbImage = value; }
        }
        public int ArticleID
        {
            get { return Record.ArticleID; }
            set { Record.ArticleID = value; }
        }
        public bool IsFeatured
        {
            get { return Record.IsFeatured; }
            set { Record.IsFeatured = value; }
        }
    }
}