using System.Collections.Generic;
using System.Web.Routing;
using Orchard;

namespace Northern.News.Routing
{
    public interface IArticlePathConstraint : IRouteConstraint, ISingletonDependency
    {
        void SetPaths(IEnumerable<string> paths);
        string FindPath(string path);
        void AddPath(string path);
        void RemovePath(string path);
    }
}