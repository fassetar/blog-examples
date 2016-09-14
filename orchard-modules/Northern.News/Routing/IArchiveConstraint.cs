using System.Web.Routing;
using Northern.News.Models;
using Orchard;

namespace Northern.News.Routing
{
    public interface IArchiveConstraint : IRouteConstraint, ISingletonDependency
    {
        string FindPath(string path);
        ArchiveData FindArchiveData(string path);
    }
}