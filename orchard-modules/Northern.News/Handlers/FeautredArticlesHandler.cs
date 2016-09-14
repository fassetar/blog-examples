using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Northern.News.Models;

namespace Northern.News.Handler
{
    public class FeaturedArticlesHandler : ContentHandler
    {
        public FeaturedArticlesHandler(IRepository<FeaturedArticlesPartRecord> featuredArticlesPartRepository)
        {
            Filters.Add(StorageFilter.For(featuredArticlesPartRepository));
        }
    }
}