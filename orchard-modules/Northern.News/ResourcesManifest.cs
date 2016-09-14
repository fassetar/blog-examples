using Orchard.UI.Resources;

namespace Northern.News
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineStyle("ArticlesArchive").SetUrl("ArticlesArchive.css");

            manifest.DefineScript("ArticlesArchive").SetUrl("ArticlesArchive.js");
        }
    }
}
