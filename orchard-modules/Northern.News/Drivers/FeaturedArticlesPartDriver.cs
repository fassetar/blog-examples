using System;
using System.Linq;
using Northern.News.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Northern.News.ViewModels;
using System.Collections.Generic;

namespace Northern.News.Drivers
{
    public class FeaturedArticlesPartDriver : ContentPartDriver<FeaturedArticlesPart>
    {
        private readonly IContentManager _contentManager;

        public FeaturedArticlesPartDriver(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        protected override DriverResult Display(FeaturedArticlesPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Article_FeaturedArticles", () =>
            {

                var articles = _contentManager.Query(VersionOptions.Published, "Article")
                    .Slice(0,0)
                    .Select(ci => ci.As<ArticlePart>())
                    .Where(ci => ci.IsFeatured == true);

                List<ArticlePart> al = new List<ArticlePart>();
                foreach (var a in articles)
                {
                    al.Add(a);
                }

                Random rng = new Random();
                int n = al.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    ArticlePart value = al[k];
                    al[k] = al[n];
                    al[n] = value;
                }

                ArticlePart[] tempArray = al.ToArray();
                int featuredCount = tempArray.Length;
                if (part.Count > featuredCount)
                    part.Count = featuredCount;
                ArticlePart[] articleArray = new ArticlePart[part.Count];

                for (int i = 0; i < part.Count; i++)
                {
                    articleArray[i] = tempArray[i];
                }

                var featured = articleArray.AsEnumerable<ArticlePart>();

                var list = shapeHelper.List();
                list.AddRange(featured.Select(a => _contentManager.BuildDisplay(a, "Summary")));

                var articleList = shapeHelper.Parts_Article_List(ContentItems: list);

                return shapeHelper.Parts_Article_FeaturedArticles(ContentItems: articleList);
            });
        }

        protected override DriverResult Editor(FeaturedArticlesPart part, dynamic shapeHelper)
        {
            var viewModel = new FeaturedArticlesViewModel
            {
                Count = part.Count
            };

            return ContentShape("Parts_Article_FeaturedArticles_Edit",
                                () => shapeHelper.EditorTemplate(TemplateName: "Parts/FeaturedArticles", Model: viewModel, Prefix: Prefix));
        }

        protected override DriverResult Editor(FeaturedArticlesPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new FeaturedArticlesViewModel();

            if (updater.TryUpdateModel(viewModel, Prefix, null, null))
            {
                part.Count = viewModel.Count;
            }

            return Editor(part, shapeHelper);
        }

    }
}