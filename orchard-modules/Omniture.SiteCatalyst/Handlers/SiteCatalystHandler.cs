using JetBrains.Annotations;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Omniture.SiteCatalyst.Models;

namespace Omniture.SiteCatalyst.Handlers
{
    [UsedImplicitly]
    public class SiteCatalystHandler : ContentHandler
    {
        public SiteCatalystHandler(IRepository<SiteCatalystPartRecord> siteCatalystPartRepository)
        {
            Filters.Add(StorageFilter.For(siteCatalystPartRepository));
        }
    }
}