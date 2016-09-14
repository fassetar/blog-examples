using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Northern.News.Models;

namespace Northern.News.ViewModels
{
    public class RecentArticlesViewModel
    {
        [Required]
        public int Count { get; set; }

        //public IEnumerable<ArticlePart> Blogs { get; set; }
    }
}