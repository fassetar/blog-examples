using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Northern.News.Models;

namespace Northern.News.Handlers
{
    public class ArticleHandler : ContentHandler {
        public ArticleHandler(IRepository<ArticlePartRecord> articlePartRepository) {
            Filters.Add(StorageFilter.For(articlePartRepository));
        }
    }
}