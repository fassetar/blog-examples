using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Northern.News.Models
{
    [XmlRootAttribute(ElementName = "NSArticles", Namespace = "", IsNullable = false)]
    public class XMLArticle
    {

        [XmlArray("articles")]

        [XmlArrayItem("article", typeof(Article))]
        public Article[] Articles { get; set; }
    }

    public class Article
    {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "ImageURL")]
        public string ImageURL { get; set; }
        [XmlElement(ElementName = "ThumbURL")]
        public string ThumbURL { get; set; }
        [XmlElement(ElementName = "IsFeatured")]
        public bool IsFeatured { get; set; }
        [XmlElement(ElementName = "IsPublished")]
        public bool IsPublished { get; set; }
        [XmlElement(ElementName = "Text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "ArticleID")]
        public int ArticleID { get; set; }
        [XmlElement(ElementName = "CreatedUtc")]
        public DateTime CreatedUtc { get; set; }
        [XmlElement(ElementName = "PublishedUtc")]
        public DateTime PublishedUtc { get; set; }
        [XmlElement(ElementName = "ModifiedUtc")]
        public DateTime ModifiedUtc { get; set; }
        [XmlElement(ElementName = "Data")]
        public string Categories { get; set; }
    }
}