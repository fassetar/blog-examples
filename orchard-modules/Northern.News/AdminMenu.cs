using Orchard.UI.Navigation;
using Orchard.Localization;

namespace Northern.News
{
    public class AdminMenu : INavigationProvider 
    {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.Add(T("Northern News"), "5", BuildMenu);
        }

        private void BuildMenu(NavigationItemBuilder menu){
            menu.Add(T("List"), "1.0", item =>
                item.Action("List", "Admin", new { area = "Contents", id = "Article" }));
            menu.Add(T("New Article"), "1.1", item =>
                item.Action("Create", "Admin", new { area = "Contents", id = "Article" }));
            menu.Add(T("Manage Brafton Feed"), "2.0", item =>
                item.Action("Brafton", "ArticleAdmin", new { area = "Northern.News" }));
            menu.Add(T("Load From XML"), "2.1", item =>
                item.Action("LoadXML", "ArticleAdmin", new { area = "Northern.News" }));
        }
    }
}
