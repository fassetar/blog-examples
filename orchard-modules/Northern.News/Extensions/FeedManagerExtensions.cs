using System.Web.Routing;
using Northern.News.Models;
using Orchard.Core.Feeds;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;

namespace Northern.News.Extensions
{
    /// <summary>A class to register the feed of title (article part) in the route value dictionary. This class give value for the dictionary which are used in routing class.</summary>
    public static class FeedManagerExtensions
    {
        /// <summary>A method to set a title to the route value dictionary whihc is the article's id.</summary>
        /// <param name="feedManager">A parameter to take the feed manager service and pass in register route values.</param>
        /// <param name="articlePart">A parameter to take article part title.</param>
        public static void Register(this IFeedManager feedManager, ArticlePart articlePart)
        {
            feedManager.Register(articlePart.As<TitlePart>().Title, "rss", new RouteValueDictionary { { "containerid", articlePart.Id } });
            feedManager.Register(articlePart.As<TitlePart>().Title + " - Comments", "rss", new RouteValueDictionary { { "commentedoncontainer", articlePart.Id } });
        }
    }
}
