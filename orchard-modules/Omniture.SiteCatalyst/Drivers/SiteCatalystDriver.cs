using Omniture.SiteCatalyst.Models;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement;
using Omniture.SiteCatalyst.ViewModels;

namespace Omniture.SiteCatalyst.Drivers
{
    /// <summary>Builds the shape for SiteCataylst ViewModel.</summary>
    public class SiteCatalystPartDriver : ContentPartDriver<SiteCatalystPart>
    {
        /// <summary>A Method to Display shapes of the Content Item.</summary>
        /// <param name="part">Gives access to content item.</param>
        /// <param name="displayType">A variable to display the type.</param>
        /// <param name="shapeHelper">A dynamic Shape builder of the content item.</param>
        /// <returns>Returns a Action Result of the view with the content item.</returns>
        protected override DriverResult Display(SiteCatalystPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_SiteCatalyst", () =>
            {
                var viewModel = new SiteCatalystViewModel
                {
                    PageName = part.PageName,
                    PageType = part.PageType
                };

                return shapeHelper.Parts_SiteCatalyst(PageName: part.PageName, PageType: part.PageType);
            });
        }

        /// <summary>Get method of the content item for edit.</summary>
        /// <param name="part">Gives access to content item.</param>
        /// <param name="shapeHelper">A dynamic Shape builder of the content item.</param>
        /// <returns>Returns view of the content item with shape helper.</returns>
        protected override DriverResult Editor(SiteCatalystPart part, dynamic shapeHelper)
        {
            var viewModel = new SiteCatalystViewModel
            {
                PageName = part.PageName,
                PageType = part.PageType
            };

            return ContentShape("Parts_SiteCatalyst_Edit", () =>
                shapeHelper.EditorTemplate(TemplateName: "Parts/SiteCatalyst", Model: viewModel, Prefix: Prefix));
        }

        /// <summary> A POST action result of the edit site catalyst part.</summary>
        /// <param name="part">Give acces to the Site Catalyst Part.</param>
        /// <param name="updater">A builder of the updated content item.</param>
        /// <param name="shapeHelper">A shape builder to display the content item</param>
        /// <returns>Returns view of the content item with shape helper.</returns>
        protected override DriverResult Editor(SiteCatalystPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new SiteCatalystViewModel();

            if (updater.TryUpdateModel(viewModel, Prefix, null, null))
            {
                part.PageName = viewModel.PageName;
                part.PageType = viewModel.PageType;
            }

            return Editor(part, shapeHelper);
        }

        /// <summary>A method for the importing a xml file in Import/Export module.</summary>
        /// <param name="part">Give acces to the Site Catalyst Part.</param>
        /// <param name="context">For Elements of a xml file addedas a Attribute Value.</param>
        protected override void Importing(SiteCatalystPart part, Orchard.ContentManagement.Handlers.ImportContentContext context)
        {
            var pageName = context.Attribute(part.PartDefinition.Name, "PageName");
            if (pageName != null)
            {
                part.PageName = pageName;
            }

            var pageType = context.Attribute(part.PartDefinition.Name, "PageType");
            if (pageType != null)
            {
                part.PageType = pageType;
            }
        }

        /// <summary>A method for the exporting a xml file in Import/Export module.</summary>
        /// <param name="part">Give acces to the Site Catalyst Part.</param>
        /// <param name="context">For Elements of a xml file addedas a Attribute Value.</param>
        protected override void Exporting(SiteCatalystPart part, Orchard.ContentManagement.Handlers.ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("PageName", part.PageName);
            context.Element(part.PartDefinition.Name).SetAttributeValue("PageType", part.PageType);
        }
    }
}