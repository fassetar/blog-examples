using Orchard.ContentManagement.Records;
namespace Northern.News.Models
{
    public class FeaturedArticlesPartRecord : ContentPartRecord
    {
        public virtual int Count { get; set; }
    }
}