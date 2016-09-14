using System;
using System.Linq;
using Northern.News.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Orchard.Data;

namespace Northern.News.Drivers
{
    public class ArticlesArchivePartDriver : ContentPartDriver<ArticlesArchivePart>
    {
        private readonly IContentManager _contentManager;
        private readonly IRepository<ArticlePartArchiveRecord> _articleArchiveRepository;

        public ArticlesArchivePartDriver(IContentManager contentManager, IRepository<ArticlePartArchiveRecord> articleArchiveRepository)
        {
            _contentManager = contentManager;
            _articleArchiveRepository = articleArchiveRepository;
        }

        //protected override DriverResult Display(ArticlesArchivePart part, string displayType, dynamic shapeHelper)
        //{

        //    return ContentShape("Parts_Article_ArticlesArchive", () =>
        //   {
        //       part.Count = 0;
        //       var articles = _contentManager.Query(VersionOptions.Published, "Article")
        //              .Slice(0, 0)
        //              .OrderByDescending(c => c.As<CommonPart>().CreatedUtc)
        //              .Select(ci => ci.As<ArticlePart>()
        //              );

        //       List<ArticlePart> al = new List<ArticlePart>();
               
        //       foreach (var a in articles)
        //       {
        //           al.Add(a);
        //           //part.Month = Convert.ToInt32(a.As<CommonPart>().CreatedUtc);
        //           //Year?
        //           part.Count++;
        //       }
        //       ArticlePart[] articleArray = al.ToArray();
        //       //var test = al[1].As<CommonPart>().CreatedUtc;
               
        //       var featured = articleArray.AsEnumerable<ArticlePart>();
        //       var list = shapeHelper.List();
        //       list.AddRange(featured.Select(a => _contentManager.BuildDisplay(a, "Summary")));

        //       var articleList = shapeHelper.Parts_Article_List(ContentItems: list);
              
        //       return shapeHelper.Parts_Article_ArticlesArchive(Archives: articleList);
        //   });
        //}

        protected override DriverResult Display(ArticlesArchivePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Article_ArticlesArchive",
                                () =>
                                {
                                    return shapeHelper.Parts_Article_ArticlesArchive(Archives: GetArchives());
                                });
        }

        public IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives()
        {
            var query =
                from bar in _articleArchiveRepository.Table
                orderby bar.Year descending, bar.Month descending
                select bar;

            return
                query.ToList().Select(
                    bar =>
                    new KeyValuePair<ArchiveData, int>(new ArchiveData(string.Format("{0}/{1}", bar.Year, bar.Month)),
                                                       bar.PostCount));
        }


        //public IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives()
        //{
        //    var query =
        //        from bar in _articleArchiveRepository.Table
        //        orderby bar.As<CommonPart>().CreatedUtc descending
        //        select bar;

        //    return
        //        query.ToList().Select(
        //            bar =>
        //            new KeyValuePair<ArchiveData, int>(new ArchiveData(string.Format("{0}/{1}", bar.As<CommonPart>().CreatedUtc.Value.Year, bar.As<CommonPart>().CreatedUtc.Value.Month)), bar.ArticleID));
        //}
    }
}