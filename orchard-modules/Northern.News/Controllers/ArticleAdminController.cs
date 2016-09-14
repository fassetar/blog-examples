using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Contrib.Taxonomies.Models;
using Contrib.Taxonomies.Services;
using NHibernate;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.Records;
using Orchard.ContentTypes.Services;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Settings;
using Orchard.Tasks.Scheduling;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using System;
using Orchard.Tasks;
using Northern.News.Models;
using Orchard.Autoroute.Models;
using Orchard.Core.Title.Models;
using System.Net;
using System.IO;
using System.Web.Hosting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Xml.Serialization;
using Orchard.Projections.Models;
using Omniture.SiteCatalyst.Models;
using System.Runtime.InteropServices;

namespace Northern.News.Controllers
{
    /// <summary>Article Admin is the everything from the Brafton feed, taxononmy creation and Loading the XML. It handles the admin menu item results with mvc.</summary>
    [ValidateInput(false), Admin]
    [ComVisible(false)]
    public class ArticleAdminController : Controller
    {
        #region ---Fields+properties---
        // ------------------------------------------------------------------------ //
        /// <summary>Intilzation of brafton api object.</summary>
        private ApiContext brafton = new ApiContext();
        /// <summary>brafton verfication api key.</summary>
        private string api_key = "470cc2ac-4c55-4cd1-b6d7-3676228f6730";
        /// <summary>a string for the base url of where brafton api server is. </summary>
        private string base_url = "http://api.brafton.com/";
        // ------------------------------------------------------------------------ //
        /// <summary>Set to Media\Default\news\{articleID}\{imageName} inorder for the method of grabbing Images.</summary>
        public string imageURL = @"Media\Default\news\{articleID}\{imageName}";
        // ------------------------------------------------------------------------ //
        /// <summary>A interface for the content manager.</summary>
        private readonly IContentManager _contentManager;
        /// <summary>A interface for the transactino manager.</summary>
        private readonly ITransactionManager _transactionManager;
        /// <summary>A interface for the Site Service</summary>
        private readonly ISiteService _siteService;
        /// <summary>A interface for the repository, with the taxonomy term related to items.</summary>
        private readonly IRepository<TermContentItem> _repository_termcontentitem;
        /// <summary>A interface for the repository, with the taxonomy terms.</summary>
        private readonly IRepository<TermPartRecord> _repository_termpart;
        /// <summary>A interface for the repository, with taxonomy part.</summary>
        private readonly IRepository<TaxonomyPartRecord> _repository_taxopart;
        /// <summary>A interface for the repository of auto route part.</summary>
        private readonly IRepository<AutoroutePartRecord> _repository_autoroute;
        /// <summary>A variable to test if the xml of old article has completed.</summary>
        public static bool IsXmlComplete = false;
        /// <summary>A interface for the taxonomy service.</summary>
        private readonly ITaxonomyService _taxo;
        #endregion
        #region ---Constructor---
        /// <summary>Creates a initilization of Article Admin Controller which controllers all the brafton feed and taxonomy of Articles.</summary>
        /// <param name="services">Connects IOrchardServices with the parameter.</param>
        /// <param name="contentManager">Connects Content manager with the parameter.</param>
        /// <param name="transactionManager">Connects ITransactionManager with the parameter.</param>
        /// <param name="siteService">Connects ISiteService with the parameter.</param>
        /// <param name="repository_termcontentitem">Connects IRepository<TermContentItem> with the parameter.</param>
        /// <param name="repository_termpart">Connects IRepository<TermPartRecord> with the parameter.</param>
        /// <param name="repository_taxopart">Connects IRepository<TaxonomyPartRecord> with the parameter.</param>
        /// <param name="repository_autoroute">Connects IRepository<AutoroutePartRecord> with the parameter.</param>
        /// <param name="taxo"></param>
        /// <param name="shapeFactory"></param>
        public ArticleAdminController(
            IOrchardServices services,
            IContentManager contentManager,
            ITransactionManager transactionManager,
            ISiteService siteService,
            IRepository<TermContentItem> repository_termcontentitem,
            IRepository<TermPartRecord> repository_termpart,
            IRepository<TaxonomyPartRecord> repository_taxopart,
            IRepository<AutoroutePartRecord> repository_autoroute,
            ITaxonomyService taxo,
            IShapeFactory shapeFactory)
        {
            Services = services;
            _contentManager = contentManager;
            _transactionManager = transactionManager;
            _repository_termcontentitem = repository_termcontentitem;
            _repository_termpart = repository_termpart;
            _repository_taxopart = repository_taxopart;
            _repository_autoroute = repository_autoroute;
            _taxo = taxo;
            _siteService = siteService;
            brafton = new ApiContext(api_key, base_url);
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }
        #endregion
        /// <summary>A dynamic builder for shapes in Articles.</summary>
        dynamic Shape { get; set; }

        /// <summary>http://docs.orchardproject.net/Documentation/How-Orchard-works#Localization </summary>
        public Localizer T { get; set; }
        /// <summary>A interface to control the Orchard Service.</summary>
        public IOrchardServices Services { get; set; }

        /// <summary>Controller for displaying the Brafton Article count, with how many are in the database (published/unpublished).</summary>
        /// <returns>A view for the Brafton page</returns>
        public ActionResult Brafton()
        {
            int article_count = brafton.News.Count();

            // get a list of all existing articles
            List<ContentItem> articles = _contentManager
                .Query(VersionOptions.AllVersions, "Article").List().ToList<ContentItem>();

            // get a list of all published articles
            List<ContentItem> articles_pubd = _contentManager
                .Query(VersionOptions.Published, "Article").List().ToList<ContentItem>();

            ViewData["count_db"] = articles.Count;
            ViewData["count_published"] = articles_pubd.Count;
            ViewData["count_feed"] = article_count;

            return View();
        }

        /// <summary>Gives ActionResult to the View of Loading Braftion Feed Page. </summary>
        /// <returns>A view for the Brafton page after hit has loaded bafton feed.</returns>
        public ActionResult LoadBrafton()
        {
            int count_articlesLoaded = 0;

            count_articlesLoaded = PullBraftonArticles();

            ViewData["count_categories"] = brafton.CategoryDefinitions.Count();
            ViewData["count_feed"] = brafton.News.Count();
            ViewData["count_loaded"] = count_articlesLoaded;

            return View();
        }

        /// <summary>A action result for xml admin menu page.</summary>
        /// <returns>Returns the view for loading the xml.</returns>
        public ActionResult XML()
        {
            return View();

        }

        /// <summary>A action result for loading xml.</summary>
        /// <returns>Return the page result of loading the xml.</returns>
        public ActionResult LoadXML()
        {
            if (_taxo.GetTaxonomyByName("ArticleTaxonomy") != null)
            {
                string url = Server.MapPath("/") + "NSArticles.xml"; //Path to the XML file
                XMLArticle XMLArticles = new XMLArticle();
                using (XmlTextReader reader = new XmlTextReader(url))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(XMLArticle));
                    XMLArticles = (XMLArticle)mySerializer.Deserialize(reader);
                }
                int count_articlesLoaded = 0;
                TaxonomyPart articleTaxo = new TaxonomyPart();
                articleTaxo = _taxo.GetTaxonomyByName("ArticleTaxonomy");


                foreach (var news in XMLArticles.Articles)
                {
                    TermPart child_node = new TermPart();
                    TermPart parent_node = new TermPart();
                    List<TermPart> termsOnArticle = new List<TermPart>();
                    termsOnArticle = new List<TermPart>();
                    // if article doesn't already exist (checks for the article id in the route path)
                    string check_slug = "News/Article/" + news.ArticleID + "/" + Slugify(news.Title);

                    if (_contentManager.Query<AutoroutePart, AutoroutePartRecord>(VersionOptions.AllVersions)
                        .Where(rtpart => rtpart.DisplayAlias == check_slug).Count() <= 0)
                    {
                        ContentItem article = _contentManager.New("Article");
                        article.As<TitlePart>().Title = news.Title;
                        article.As<AutoroutePart>().DisplayAlias = "News/Article/" + news.ArticleID + "/" + Slugify(news.Title);
                        article.As<BodyPart>().Text = news.Text;
                        article.As<CommonPart>().CreatedUtc = news.CreatedUtc;
                        article.As<ArticlePart>().ArticleID = news.ArticleID;
                        article.As<ArticlePart>().IsFeatured = news.IsFeatured;
                        article.As<SiteCatalystPart>().PageName = "Article:" + news.Title;
                        article.As<SiteCatalystPart>().PageType = "Article";
                        #region //Image scrap section//
                        if (!String.IsNullOrEmpty(news.ImageURL))
                        {
                            // delimit to grab image name
                            int slashIndex = news.ImageURL.LastIndexOf("/") + 1;
                            string imageName = news.ImageURL.Substring(slashIndex, news.ImageURL.Length - slashIndex);

                            // create new url for image
                            string localImageURL = imageURL.Replace("{articleID}", news.ArticleID.ToString())
                                                           .Replace("{imageName}", imageName);
                            // call image scraper to save Brafton's image locally
                            ScrapeImage(news.ImageURL, localImageURL);
                            article.As<ArticlePart>().PrimaryImage = localImageURL.Replace("\\", "/");

                            //Grabbing Thumb image///

                            int slashIndex2 = news.ThumbURL.LastIndexOf("/") + 1;
                            string imageName2 = news.ThumbURL.Substring(slashIndex2, news.ThumbURL.Length - slashIndex2);
                            // create new url for image
                            string localImageURL2 = imageURL.Replace("{articleID}", news.ArticleID.ToString())
                                                           .Replace("{imageName}", imageName2);

                            // call image scraper to save Brafton's image locally
                            ScrapeImage(news.ThumbURL, localImageURL2);
                            article.As<ArticlePart>().ThumbImage = localImageURL2.Replace("\\", "/");
                        }
                        //var mediaFolders = _mediaService.GetMediaFolders(news.ArticleId.ToString());
                        //var mediaFiles = string.IsNullOrEmpty(mediaPath) ? null : _mediaService.GetMediaFiles(mediaPath);
                        //var model = new MediaFolderEditViewModel { FolderName = name, MediaFiles = mediaFiles, MediaFolders = mediaFolders, MediaPath = mediaPath };
                        // save the article content item
                        if (news.IsPublished)
                            _contentManager.Create(article, VersionOptions.Published);
                        else
                            _contentManager.Create(article, VersionOptions.Draft);
                        _contentManager.Flush();
                        #endregion
                        #region //terms//
                        if (!String.IsNullOrEmpty(news.Categories))
                        {
                            int pipe_index = 0;
                            string parent = "";
                            string child = "";
                            // there's a parent and child
                            if (news.Categories.Contains('|'))
                            {
                                pipe_index = news.Categories.IndexOf('|');
                                parent = news.Categories.Substring(0, pipe_index);
                                child = news.Categories.Substring(pipe_index + 1, news.Categories.Length - (pipe_index + 1));

                                child_node = _taxo.GetTermByName(articleTaxo.Id, child);
                                termsOnArticle.Add(child_node);
                            }
                            else // single category is the parent
                            {
                                parent = news.Categories;
                            }

                            parent_node = _taxo.GetTermByName(articleTaxo.Id, parent);
                            termsOnArticle.Add(parent_node);
                            _taxo.UpdateTerms(article, termsOnArticle, "ArticleTaxonomy");
                        }
                    }
                    count_articlesLoaded++;
                }
                trimArticleTaxonomy();
                ViewBag.ArticleCount = count_articlesLoaded;
                IsXmlComplete = true;
            }
            else
            {
                ViewBag.ArticleCount = 0;
            }
                        #endregion
            return View();
        }

        /// <summary>The Method that uses the brafton feed to create articles and creating taxonomy terms. Also scraps images and stores them locally for orchard.</summary>
        /// <returns>Returns a count of how many article are pulished in the repository.</returns>
        public int PullBraftonArticles()
        {
            int count_articlesLoaded = 0;
            TaxonomyPart articleTaxo = new TaxonomyPart();
            // check to see if our ArticleTaxonomy already exists
            // will build a complete Taxonomy tree based on Brafton's categoryDefinitions page
            articleTaxo = updateArticleTaxonomy();
            #region //--Image Scrap//
            foreach (newsItem news in brafton.News)
            {
                // if article doesn't already exist (checks for the article id in the route path)
                string check_slug = "News/Article/" + news.id + "/" + Slugify(news.headline);

                if (_contentManager.Query<AutoroutePart, AutoroutePartRecord>(VersionOptions.AllVersions)
                    .Where(rtpart => rtpart.DisplayAlias == check_slug).Count() <= 0)
                {
                    ContentItem article = _contentManager.New("Article");
                    article.As<TitlePart>().Title = news.headline;
                    article.As<AutoroutePart>().DisplayAlias = "News/Article/" + news.id + "/" + Slugify(news.headline);
                    article.As<BodyPart>().Text = news.text;
                    article.As<CommonPart>().CreatedUtc = news.createdDate;
                    article.As<ArticlePart>().ArticleID = news.id;
                    article.As<SiteCatalystPart>().PageName = "Article:" + news.headline;
                    article.As<SiteCatalystPart>().PageType = "Article";
                    // set article images
                    if (news.photos != null)
                    {
                        foreach (photo photoContainer in news.photos)
                        {
                            foreach (photo.Instance img in photoContainer.Instances)
                            {
                                // delimit to grab image name
                                int slashIndex = img.url.LastIndexOf("/") + 1;
                                string imageName = img.url.Substring(slashIndex, img.url.Length - slashIndex);

                                // create new url for image
                                string localImageURL = imageURL.Replace("{articleID}", news.id.ToString())
                                                               .Replace("{imageName}", imageName);

                                // call image scraper to save Brafton's image locally
                                ScrapeImage(img.url, localImageURL);

                                switch (img.type.ToString())
                                {
                                    case "Medium":
                                        article.As<ArticlePart>().PrimaryImage = localImageURL.Replace("\\", "/");
                                        break;

                                    case "Small":
                                        article.As<ArticlePart>().ThumbImage = localImageURL.Replace("\\", "/");
                                        break;
                                };
                            }
                        }
                    }
            #endregion
                    #region //--Taxonomy Tag Section///
                    // save the article content item
                    _contentManager.Create(article, VersionOptions.Published);
                    _contentManager.Flush();

                    //Article Taxonomy//
                    List<TermPart> termsOnArticle = new List<TermPart>();
                    TermPart child_node = new TermPart();
                    TermPart parent_node = new TermPart();

                    foreach (category cat in news.categories)
                    {
                        int pipe_index = 0;
                        string parent = "";
                        string child = "";
                        termsOnArticle = new List<TermPart>();

                        // there's a parent and child
                        if (cat.name.Contains('|'))
                        {
                            pipe_index = cat.name.IndexOf('|');
                            parent = cat.name.Substring(0, pipe_index);
                            child = cat.name.Substring(pipe_index + 1, cat.name.Length - (pipe_index + 1));
                            child_node = _taxo.GetTermByName(articleTaxo.Id, child);
                            termsOnArticle.Add(child_node);
                        }
                        else // single category is the parent
                        {
                            parent = cat.name;
                        }

                        parent_node = _taxo.GetTermByName(articleTaxo.Id, parent);
                        termsOnArticle.Add(parent_node);
                        _taxo.UpdateTerms(article, termsOnArticle, "ArticleTaxonomy");
                    }

                    count_articlesLoaded++;
                }
            }

            //trimArticleTaxonomy();
                    #endregion
            return count_articlesLoaded;
        }

        /// <summary>A method to update taxonomy by creating new one and connecting content items to them.</summary>
        /// <returns>Returns the model of taxonomy and terms.</returns>
        public TaxonomyPart updateArticleTaxonomy()
        {
            TaxonomyPart articleTaxo = new TaxonomyPart();
            int pipe_index = 0;
            string parent = "";
            string child = "";

            // check to see if our ArticleTaxonomy already exists
            articleTaxo = _taxo.GetTaxonomyByName("ArticleTaxonomy");

            // if not, build it before importing articles
            if (articleTaxo == null)
            {
                articleTaxo = _contentManager.New<TaxonomyPart>("Taxonomy");
                articleTaxo.Name = "ArticleTaxonomy";
                articleTaxo.Slug = "News-Category";

                _taxo.CreateTermContentType(articleTaxo);
                _contentManager.Create(articleTaxo);
                if (_contentManager != null)
                {
                    _contentManager.Flush();
                }
            }


            #region //Build nodes//
            // ------------------------------------------ //
            // BUILD NEW NODES IF NOT PRESENT IN TAXONOMY //
            // ------------------------------------------ //
            /* EXAMPLE:
             * <category>
             * <id>800096566</id>
             * <name>Disaster Preparedness|Earthquake</name>
             * </category>
             */
            foreach (category defcat in brafton.CategoryDefinitions)
            {
                bool hasChild = true;

                // check for pipe '|' character => has child category
                if (defcat.name.Contains('|'))
                {
                    pipe_index = defcat.name.IndexOf('|');
                    parent = defcat.name.Substring(0, pipe_index);
                    child = defcat.name.Substring(pipe_index + 1, defcat.name.Length - (pipe_index + 1));
                }
                else // single category is the parent
                {
                    parent = defcat.name;
                    hasChild = false;
                }

                // add parent to taxonomy tree since is new and it or its child has associated article(s)
                TermPart check_parent = _taxo.GetTermByName(articleTaxo.Id, parent);
                if (check_parent == null)
                {
                    check_parent = _taxo.NewTerm(articleTaxo);
                    check_parent.Name = parent;
                    check_parent.Selectable = true;
                    check_parent.Container = articleTaxo.ContentItem;
                    _taxo.ProcessPath(check_parent);
                    _contentManager.Create(check_parent);
                    _contentManager.Flush();
                }

                if (hasChild)
                {
                    // add child to taxonomy tree if new
                    TermPart check_child = _taxo.GetChildren(check_parent)
                                            .Where(x => x.Name == child).SingleOrDefault();
                    if (check_child == null)
                    {
                        check_child = _taxo.NewTerm(articleTaxo);
                        check_child.Name = child;
                        check_child.Selectable = true;
                        check_child.Container = check_parent.ContentItem;
                        _taxo.ProcessPath(check_child);
                        _contentManager.Create(check_child);
                        _contentManager.Flush();
                    }

                }
            }
            #endregion

            return articleTaxo;
        }

        /// <summary>A method to clean up taxonomy. Should not run when xml hasn't and the taxonomy should be build before loading xml. </summary>
        public void trimArticleTaxonomy()
        {
            TaxonomyPart articleTaxo = _taxo.GetTaxonomyByName("ArticleTaxonomy");
            List<string> taxoParents = new List<string>();
            bool hasChild = true;
            int pipe_index = 0;
            string parent = "";
            string child = "";

            // ---------------- //
            // TRIM CHILD NODES //
            // ---------------- //
            foreach (category defcat in brafton.CategoryDefinitions)
            {
                // check for pipe '|' character => has child category
                if (defcat.name.Contains('|'))
                {
                    pipe_index = defcat.name.IndexOf('|');
                    parent = defcat.name.Substring(0, pipe_index);
                    child = defcat.name.Substring(pipe_index + 1, defcat.name.Length - (pipe_index + 1));
                    hasChild = true;
                }
                else // single category is the parent
                {
                    parent = defcat.name;
                    hasChild = false;
                }

                // save parent so can check after all child nodes have been trimmed
                if (!taxoParents.Contains(parent))
                    taxoParents.Add(parent);

                // check if child has existing associations
                // children nodes may not be unique across entire Taxonomy
                // so must check the one tied to this parent
                if (hasChild)
                {
                    TermPart parent_node = _taxo.GetTermByName(articleTaxo.Id, parent);
                    TermPart child_node = _taxo.GetChildren(parent_node)
                                            .Where(x => x.Name == child).SingleOrDefault();
                    if (_repository_termcontentitem
                        .Fetch(x => x.TermRecord.Id == child_node.Id).Count() == 0)
                    {
                        // --------------------------------------------- //
                        // HARD DELETE CHILD NODE NO ASSOCIATED ARTICLES //
                        // --------------------------------------------- //
                        // if after the newest articles have been loaded //
                        // a child node still does not have associated   //
                        // articles, it should be deleted from the tree  //
                        // --------------------------------------------- //
                        _repository_termpart.Delete(child_node.Record);
                        _repository_termpart.Flush();

                        _repository_autoroute.Delete(child_node.As<AutoroutePart>().Record);
                        _repository_autoroute.Flush();
                    }
                }
            }

            // ----------------- //
            // TRIM PARENT NODES //
            // ----------------- //
            foreach (string rent in taxoParents)
            {
                TermPart parent_node = _taxo.GetTermByName(articleTaxo.Id, rent);

                // check if parent has any article associations
                if (_repository_termcontentitem
                    .Fetch(x => x.TermRecord.Id == parent_node.Id).Count() == 0)
                {
                    // --------------------------------------------- //
                    // HARD DELETE PARENTS IF NO ASSOCIATED ARTICLES //
                    // --------------------------------------------- //
                    // child nodes have been stripped above so if a  //
                    // a single parent node still exists & it does   //
                    // not have associated articles, delete it       //
                    // --------------------------------------------- //
                    _repository_termpart.Delete(parent_node.Record);
                    _repository_termpart.Flush();

                    _repository_autoroute.Delete(parent_node.As<AutoroutePart>().Record);
                    _repository_autoroute.Flush();
                }
            }
        }

        /// <summary>Creates slug used in the path for Article urls.</summary>
        /// <param name="slug">A slug is some route scheme used in the url. </param>
        /// <returns></returns>
        private static string Slugify(string slug)
        {
            var dissallowed = new Regex(@"[/:?#\[\]@!$&'()*+,;=\s]+");

            slug = dissallowed.Replace(slug, "-");
            slug = slug.Replace("\"", "");
            slug = slug.Trim('-');

            if (slug.Length > 1000)
                slug = slug.Substring(0, 1000);

            return slug.ToLowerInvariant();
        }

        /// <summary>A method that takes urls and stores a image locally.</summary>
        /// <param name="braftonImageURL">The URL of a brafton image before it has be scrapped.</param>
        /// <param name="localImageURL">The URL of a image after it has be stored locally.</param>
        /// <returns>Returns a true if scrapping worked and false if there was an error.</returns>
        public bool ScrapeImage(string braftonImageURL, string localImageURL)
        {
            Uri address;

            if (string.IsNullOrWhiteSpace(braftonImageURL) || string.IsNullOrWhiteSpace(localImageURL))
            {
                return false;
            }

            if (Uri.TryCreate(braftonImageURL, UriKind.RelativeOrAbsolute, out address))
            {
                using (var downloader = new WebClient())
                {
                    try
                    {
                        byte[] imageBytes = downloader.DownloadData(address);
                        MemoryStream memStream = new MemoryStream(imageBytes);
                        Image image = Image.FromStream(memStream);

                        localImageURL = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, localImageURL);

                        FileInfo imageInfo = new FileInfo(localImageURL);

                        if (!imageInfo.Directory.Exists)
                        {
                            Directory.CreateDirectory(imageInfo.Directory.FullName);
                        }

                        image.Save(imageInfo.FullName, ImageFormat.Jpeg);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
