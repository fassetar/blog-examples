using Orchard.ContentManagement;

namespace Northern.News.Models
{
    public class FeaturedArticlesPart : ContentPart<FeaturedArticlesPartRecord>
    {
        public int Count
        {
            get { return Record.Count; }
            set { Record.Count = value; }
        }
    }
}