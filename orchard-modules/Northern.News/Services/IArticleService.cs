using System;
using System.Collections.Generic;
using Northern.News.Models;
using Orchard.ContentManagement;
using Orchard;
using Orchard.Core.Common.Models;

namespace Northern.News.Services
{
    public interface IArticleService : IDependency
    {
        IEnumerable<ArticlePart> Get(VersionOptions versionOptions);
        IEnumerable<ArticlePart> Get(ArchiveData archiveData);
        IContentQuery<ContentItem, CommonPartRecord> GetArticleQuery(VersionOptions versionOptions);
    }
}