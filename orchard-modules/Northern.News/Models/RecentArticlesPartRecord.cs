using Orchard.ContentManagement.Records;
namespace Northern.News.Models
{
    public class RecentArticlesPartRecord : ContentPartRecord
    {
        public virtual int Count { get; set; }
    }
}