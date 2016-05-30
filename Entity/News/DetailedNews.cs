using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Shared;

namespace Entity.News
{
    /// <summary>
    /// Entity News
    /// Dùng để trả về một bài viết cụ thể
    /// </summary>
    public class DetailedNews : News
    {
        public string Content { set; get; }
        public List<string> TagList { set; get; }
        public List<Comment> CommentList { set; get; }
        public List<string> UrlList { set; get; }        

        public DetailedNews(string postId, string description, List<string> imgUrlList, string writer, string place, string location, Rate rate, string content, List<string> tagList, List<Comment> cmtList, List<Category> categoryList, List<string> urlList)
            : base(postId, description, imgUrlList, writer, place, location, rate, categoryList)
        {
            this.Content = content;
            this.TagList = tagList;
            this.CommentList = cmtList;
            this.UrlList = urlList;
        }

        public DetailedNews() { }
    }
}
