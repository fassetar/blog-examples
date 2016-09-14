using System;
using System.Linq;
using Northern.News.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Northern.News.ViewModels;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Northern.News.Drivers
{
    /// <summary>build shapes to be rendered by the theme engine of Recent Articles Widget.</summary>
    public class RecentArticlesPartDriver : ContentPartDriver<RecentArticlesPart>
    {
        /// <summary>A interface for controlling the content manager.</summary>
        private readonly IContentManager _contentManager;

        /// <summary>Creates a instance of the Recent article Widget.</summary>
        /// <param name="contentManager">Passes in the Content manager to to control Recent articles items.</param>
        public RecentArticlesPartDriver(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        /// <summary>A Method to Display shapes of the Content Item. </summary>
        /// <param name="part">Gives access to content item.</param>
        /// <param name="displayType">A variable to display the type.</param>
        /// <param name="shapeHelper">A dynamic Shape builder of the content item.</param>
        /// <returns>Returns a Action Result of the view with the content items in a list.(ordered by descending date).</returns>
        protected override DriverResult Display(RecentArticlesPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Article_RecentArticles", () =>
            {

                var articles = _contentManager.Query(VersionOptions.Published, "Article")
                    .Slice(0, 0)
                    .OrderByDescending(c => c.As<CommonPart>().CreatedUtc)
                    .Select(ci => ci.As<ArticlePart>()).Take(part.Count);

                var list = shapeHelper.List();
                list.AddRange(articles.Select(a => _contentManager.BuildDisplay(a, "Summary")));

                var articleList = shapeHelper.Parts_Article_List(ContentItems: list);

                return shapeHelper.Parts_Article_RecentArticles(ContentItems: articleList);
            });
        }
        /// <summary>Get method of the content item for edit.</summary>
        /// <param name="part">Gives access to content item.</param>
        /// <param name="shapeHelper">A dynamic Shape builder of the content item.</param>
        /// <returns>Returns an GET action result for the edit page on recent article widget.</returns>
        protected override DriverResult Editor(RecentArticlesPart part, dynamic shapeHelper)
        {
            var viewModel = new RecentArticlesViewModel
            {
                Count = part.Count
            };

            return ContentShape("Parts_Article_RecentArticles_Edit",
                                () => shapeHelper.EditorTemplate(TemplateName: "Parts/RecentArticles", Model: viewModel, Prefix: Prefix));
        }

        /// <summary>POST Method of the content item for edit.</summary>
        /// <param name="part">Gives access to content item.</param>
        /// <param name="updater">A Interface update of the model in the content item.</param>
        /// <param name="shapeHelper">A dynamic Shape builder of the content item.</param>
        /// <returns>Returns an POST action result for the edit page on recent article widget.</returns>
        protected override DriverResult Editor(RecentArticlesPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new RecentArticlesViewModel();

            if (updater.TryUpdateModel(viewModel, Prefix, null, null))
            {
                part.Count = viewModel.Count;
            }

            return Editor(part, shapeHelper);
        }
        /// <summary>A method for the importing a xml file in Import/Export module.</summary>
        /// <param name="part">Give acces to the Recent Articles Widget.</param>
        /// <param name="context">For Elements of a xml file addedas a Attribute Value.</param>
        protected override void Importing(RecentArticlesPart part, ImportContentContext context)
        {
            var count = context.Attribute(part.PartDefinition.Name, "Count");
            if (count != null)
            {
                part.Count = Convert.ToInt32(count);
            }
            
        }

        /// <summary>A method for the exporting a xml file in Import/Export module.</summary>
        /// <param name="part">Give acces to the Recent Articles Widget.</param>
        /// <param name="context">For Elements of a xml file addedas a Attribute Value.</param>
        protected override void Exporting(RecentArticlesPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Count", part.Count);
        }
    }
}
