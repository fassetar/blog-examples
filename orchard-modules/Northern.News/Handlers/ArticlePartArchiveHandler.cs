using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Northern.News.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Northern.News.Services;

namespace Northern.News.Handlers
{
    [UsedImplicitly]
    public class ArticlePartArchiveHandler : ContentHandler
    {
        public ArticlePartArchiveHandler(IRepository<ArticlePartArchiveRecord> articleArchiveRepository, IArticleService articleService)
        {
            OnPublished<ArticlePart>((context, ap) => RecalculateArticlesArchive(articleArchiveRepository, articleService, ap));
            OnUnpublished<ArticlePart>((context, ap) => RecalculateArticlesArchive(articleArchiveRepository, articleService, ap));
            OnRemoved<ArticlePart>((context, ap) => RecalculateArticlesArchive(articleArchiveRepository, articleService, ap));
        }

        private static void RecalculateArticlesArchive(IRepository<ArticlePartArchiveRecord> articleArchiveRepository, IArticleService articleService, ArticlePart articlePart)
        {
            articleArchiveRepository.Flush();

            // remove all current blog archive records
            var articleArchiveRecords =
                from bar in articleArchiveRepository.Table
                select bar;
            articleArchiveRecords.ToList().ForEach(articleArchiveRepository.Delete);

            // get all blog posts for the current blog
            var articles = articleService.Get(VersionOptions.Published);

            // create a dictionary of all the year/month combinations and their count of posts that are published in this blog
            var inMemoryArticleArchives = new Dictionary<DateTime, int>(articles.Count());
            foreach (var post in articles)
            {
                if (!post.Has<CommonPart>())
                    continue;

                var commonPart = post.As<CommonPart>();
                var key = new DateTime(commonPart.CreatedUtc.Value.Year, commonPart.CreatedUtc.Value.Month, 1);

                if (inMemoryArticleArchives.ContainsKey(key))
                    inMemoryArticleArchives[key]++;
                else
                    inMemoryArticleArchives[key] = 1;
            }

            // create the new blog archive records based on the in memory values
            foreach (KeyValuePair<DateTime, int> item in inMemoryArticleArchives)
            {
                articleArchiveRepository.Create(new ArticlePartArchiveRecord { Year = item.Key.Year, Month = item.Key.Month, PostCount = item.Value });
            }
        }
    }
}