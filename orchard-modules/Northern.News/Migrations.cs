using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.ContentManagement.MetaData;

namespace Northern.News
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterTypeDefinition("Article", builder =>
                builder.WithPart("CommonPart", p => p.WithSetting("DateEditorSettings.ShowDateEditor", "true"))
                .WithPart("TitlePart")
                .WithPart("AutoroutePart"));
            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition("Article", builder =>
                builder.WithPart("BodyPart")
                .Creatable()
                .Draftable());
            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition("Article", builder =>
                builder
                    .WithPart("BodyPart", partBuilder =>
                        partBuilder.WithSetting("BodyTypePartSettings.Flavor", "text")));
            return 3;
        }

        public int UpdateFrom3()
        {
            SchemaBuilder.CreateTable("ArticlePartRecord", table =>
                table.ContentPartRecord()
                    .Column<string>("PrimaryImage")
                    .Column<string>("ThumbImage")
                    .Column<int>("ArticleID")
                    .Column<bool>("IsFeatured"));

            ContentDefinitionManager.AlterTypeDefinition("Article", builder =>
                builder.WithPart("ArticlePart"));
            return 4;
        }

        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterPartDefinition("ArticlePart", builder =>
                builder.WithField("ArticleTaxonomy", fld =>
                    fld.OfType("TaxonomyField")
                    .WithSetting("DisplayName", "ArticleTaxonomy")
                    .WithSetting("TaxonomyFieldSettings.Taxonomy", "ArticleTaxonomy")
                    .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                    .WithSetting("TaxonomyFieldSettings.SingleChoice", "False")
                    .WithSetting("TaxonomyFieldSettings.Hint", "Select as many Categories as apply to this Article.")));


            SchemaBuilder.CreateTable("FeaturedArticlesPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<int>("Count")
                );
            ContentDefinitionManager.AlterTypeDefinition("FeaturedArticles",
                cfg => cfg
                    .WithPart("FeaturedArticlesPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );
            return 5;
        }

        public int UpdateFrom5()
        {
            SchemaBuilder.CreateTable("RecentArticlesPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<int>("Count")
                );
            ContentDefinitionManager.AlterTypeDefinition("RecentArticles",
                cfg => cfg
                    .WithPart("RecentArticlesPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            SchemaBuilder.CreateTable("ArticlePartArchiveRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("Year")
                    .Column<int>("Month")
                    .Column<int>("PostCount")
                    );

            ContentDefinitionManager.AlterTypeDefinition("ArticlesArchive",
                cfg => cfg
                    .WithPart("ArticlesArchivePart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));
            return 6;
        }

        public int UpdateFrom6()
        {
            SchemaBuilder.CreateTable("ArticlesArchivePartRecord",
                table => table
                    .ContentPartRecord()
                );

            ContentDefinitionManager.AlterTypeDefinition("Article", builder =>
                builder.WithPart("SiteCatalystPart"));
            return 7;
        }
    }
}