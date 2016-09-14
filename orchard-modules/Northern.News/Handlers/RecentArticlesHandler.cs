using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Northern.News.Models;

namespace Northern.News.Handler
{
    public class RecentArticlesHandler : ContentHandler
    {
        public RecentArticlesHandler(IRepository<RecentArticlesPartRecord> recentArticlesPartRepository)
        {
            Filters.Add(StorageFilter.For(recentArticlesPartRepository));
        }
    }
}