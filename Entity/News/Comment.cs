using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.News;

namespace Entity.News
{
    /// <summary>
    /// Entity Comment
    /// Thông tin một bình luận
    /// 
    /// CommentId : Id của comment, convention bắt đầu từ ID của bài viết + số đếm tăng dần, khi add comment vào bài viết sẽ tự gen comment id
    /// MemberComment : Thông tin người viết comment
    /// Content : Nội dung bình luận
    /// TotalLike : Tổng like bình luận
    /// IsLiked : Flag đánh đấu Current Member đã like chưa (check trong DB MemberId có ở trong List MemberLike hay không)
    /// IsProved: comment đã đc lọc hay chưa
    /// </summary>
    public class Comment
    {
        public string ID { set; get; }
        public string Writer { set; get; }
        public string Content { set; get; }
        public int TotalLike { set; get; }
        public bool IsLiked { set; get; }
        public bool IsApproved { set; get; }

        public Comment(string writer, string content)
        {
            this.Writer = writer;
            this.Content = content;
        }

        public Comment() { }
    }
}
