using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Northern.News.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Data.Conventions;
using Orchard.Tasks.Scheduling;

namespace Northern.News.Services
{
    [UsedImplicitly]
    public class ArticleService : IArticleService
    {
        private readonly IContentManager _contentManager;
        private readonly IRepository<ArticlePartArchiveRecord> _articleArchiveRepository;
        private readonly IPublishingTaskManager _publishingTaskManager;

        public ArticleService(
            IContentManager contentManager,
            IRepository<ArticlePartArchiveRecord> articleArchiveRepository, 
            IPublishingTaskManager publishingTaskManager,
            IContentDefinitionManager contentDefinitionManager) {
            _contentManager = contentManager;
            _articleArchiveRepository = articleArchiveRepository;
            _publishingTaskManager = publishingTaskManager;
        }

        public IEnumerable<ArticlePart> Get(VersionOptions versionOptions)
        {
            return GetArticleQuery(versionOptions).List().Select(ci => ci.As<ArticlePart>());
        }

        public IEnumerable<ArticlePart> Get(ArchiveData archiveData)
        {
            var query = GetArticleQuery(VersionOptions.Published);

            if (archiveData.Day > 0)
            {
                var dayDate = new DateTime(archiveData.Year, archiveData.Month, archiveData.Day);

                query = query.Where(cr => cr.CreatedUtc >= dayDate && cr.CreatedUtc < dayDate.AddDays(1));
            }
            else if (archiveData.Month > 0)
            {
                var monthDate = new DateTime(archiveData.Year, archiveData.Month, 1);

                query = query.Where(cr => cr.CreatedUtc >= monthDate && cr.CreatedUtc < monthDate.AddMonths(1));
            }
            else
            {
                var yearDate = new DateTime(archiveData.Year, 1, 1);

                query = query.Where(cr => cr.CreatedUtc >= yearDate && cr.CreatedUtc < yearDate.AddYears(1));
            }

            return query.List().Select(ci => ci.As<ArticlePart>());
        }

        public IContentQuery<ContentItem, CommonPartRecord> GetArticleQuery(VersionOptions versionOptions)
        {
            return
            //    _contentManager.Query(versionOptions, "Article")
            //    .Join<CommonPartRecord>();

            _contentManager.Query(versionOptions, "Article")
            .Join<CommonPartRecord>().OrderByDescending(cr => cr.CreatedUtc)
            .WithQueryHintsFor("Article")
                ;
        }
    }
}