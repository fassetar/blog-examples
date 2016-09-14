using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Northern.News.Models;

namespace Northern.News.Drivers
{
    public class ArticlePartDriver : ContentPartDriver<ArticlePart> {

        protected override string Prefix
        {
            get
            {
                return "Article";
            }
        }

        protected override DriverResult Display(ArticlePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Article",
                () => shapeHelper.Parts_Article(ArticlePart: part));
        }

        // GET
        protected override DriverResult Editor(ArticlePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Article_Edit", () =>
                shapeHelper.EditorTemplate(TemplateName: "Parts/Article", Model: part, Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(ArticlePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}