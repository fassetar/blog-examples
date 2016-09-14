using System.Linq;
using System.Web.Mvc;
using Northern.News.Extensions;
using Northern.News.Models;
using Northern.News.Routing;
using Northern.News.Services;
using Orchard.Core.Feeds;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Themes;
using Orchard;

namespace Northern.News.Controllers
{
    [Themed]
    public class ArticleController : Controller
    {
        private readonly IOrchardServices _services;
        private readonly IArticleService _articleService;
        private readonly IFeedManager _feedManager;
        private readonly IArchiveConstraint _archiveConstraint;

        public ArticleController(
            IOrchardServices services,
            IArticleService articleService,
            IFeedManager feedManager,
            IShapeFactory shapeFactory,
            IArchiveConstraint archiveConstraint)
        {
            _services = services;
            _articleService = articleService;
            _feedManager = feedManager;
            _archiveConstraint = archiveConstraint;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public ActionResult ListByArchive(string path)
        {

            var blogPath = _archiveConstraint.FindPath(path);
            var archive = _archiveConstraint.FindArchiveData(path);

            if (archive == null)
                return HttpNotFound();

            var list = Shape.List();
            list.AddRange(_articleService.Get(archive).Select(b => _services.ContentManager.BuildDisplay(b, "Summary")));

            dynamic viewModel = Shape.ViewModel()
                .ContentItems(list)
                .ArchiveData(archive);

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)viewModel);
        }
    }
}