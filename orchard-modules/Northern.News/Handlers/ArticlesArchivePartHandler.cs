using Northern.News.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using JetBrains.Annotations;

namespace Northern.News.Handlers
{
    [UsedImplicitly]
    public class ArticlesArchivePartHandler : ContentHandler
    {
        public ArticlesArchivePartHandler(IRepository<ArticlesArchivePartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}