using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.News;

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
        public bool IsApproved { set; get; }

        public DetailedNews(string postId, string description, List<string> imgUrlList, string writer,
            string place, string location, Rate rate, string content, List<string> tagList, List<Comment> cmtList,
            List<string> categoryList, DateTime dt, bool isApproved = false)
            : base(postId, description, imgUrlList, writer, place, location, rate, categoryList, dt)
        {
            this.Content = content;
            this.TagList = tagList;
            this.CommentList = cmtList;
            this.IsApproved = isApproved;
        }

        public DetailedNews() { }
    }
}
