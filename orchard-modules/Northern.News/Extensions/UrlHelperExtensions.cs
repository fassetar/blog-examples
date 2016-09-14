using System.Web;
using System.Web.Mvc;
using Northern.News.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Mvc.Extensions;

namespace Northern.News.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ArticleArchiveYear(this UrlHelper urlHelper, int year)
        {
            return urlHelper.Action("ListByArchive", "Article", new { path =  "archive/" + year.ToString(), area = "Northern.News" });
        }

        public static string ArticleArchiveMonth(this UrlHelper urlHelper, int year, int month)
        {
            return urlHelper.Action("ListByArchive", "Article", new { path = "archive/" + string.Format("{0}/{1}", year, month), area = "Northern.News" });
        }

        public static string ArticleArchiveDay(this UrlHelper urlHelper, int year, int month, int day)
        {
            return urlHelper.Action("ListByArchive", "Article", new { path = "archive/" + string.Format("{0}/{1}/{2}", year, month, day), area = "Northern.News" });
        }
    }
}