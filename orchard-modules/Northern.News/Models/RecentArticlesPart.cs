using Orchard.ContentManagement;

namespace Northern.News.Models
{
    public class RecentArticlesPart : ContentPart<RecentArticlesPartRecord>
    {
        public int Count
        {
            get { return Record.Count; }
            set { Record.Count = value; }
        }
    }
}