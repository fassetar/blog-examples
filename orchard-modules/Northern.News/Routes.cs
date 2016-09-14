using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;
using Northern.News.Routing;

namespace Northern.News
{
    public class Routes : IRouteProvider 
    {
        private readonly IArchiveConstraint _archiveConstraint;

        public Routes(
            IArchiveConstraint archiveConstraint ){
            _archiveConstraint = archiveConstraint;
        }
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                
                                 new RouteDescriptor {
                                                         Route = new Route(
                                                             "Admin/Articles/LoadBrafton",
                                                             new RouteValueDictionary {
                                                                                          {"area", "Northern.News"},
                                                                                          {"controller", "ArticleAdmin"},
                                                                                          {"action", "LoadBrafton"}
                                                                                      },
                                                             new RouteValueDictionary (),
                                                             new RouteValueDictionary {
                                                                                          {"area", "Northern.News"}
                                                                                      },
                                                             new MvcRouteHandler())
                                                     },
                                 new RouteDescriptor {
                                                         Route = new Route(
                                                             "Admin/Articles/Brafton",
                                                             new RouteValueDictionary {
                                                                                          {"area", "Northern.News"},
                                                                                          {"controller", "ArticleAdmin"},
                                                                                          {"action", "Brafton"}
                                                                                      },
                                                             new RouteValueDictionary (),
                                                             new RouteValueDictionary {
                                                                                          {"area", "Northern.News"}
                                                                                      },
                                                             new MvcRouteHandler())
                                                     },
                                new RouteDescriptor {
                                                         Route = new Route(
                                                             "Admin/Articles/LoadXML",
                                                             new RouteValueDictionary {
                                                                                          {"area", "Northern.News"},
                                                                                          {"controller", "ArticleAdmin"},
                                                                                          {"action", "LoadXML"}
                                                                                      },
                                                             new RouteValueDictionary (),
                                                             new RouteValueDictionary {
                                                                                          {"area", "Northern.News"}
                                                                                      },
                                                             new MvcRouteHandler())
                                                     },
                             new RouteDescriptor {
                                                     Route = new Route(
                                                         "News/{*path}",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Northern.News"},
                                                                                      {"controller", "Article"},
                                                                                      {"action", "ListByArchive"}
                                                                                  },
                                                         new RouteValueDictionary {
                                                                                      {"path", _archiveConstraint},
                                                                                  },
                                                         new RouteValueDictionary {
                                                                                      {"area", "Northern.News"}
                                                                                  },
                                                         new MvcRouteHandler())
                                                 }
                };
        }
    }
}
