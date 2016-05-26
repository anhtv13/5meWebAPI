using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Shared;

namespace Entity.News
{
	/// <summary>
	/// Entity Comment
	/// Thông tin một bình luận
	/// 
	/// CommentId : Id của comment
	/// MemberComment : Thông tin người viết comment
	/// Content : Nội dung bình luận
	/// TotalLike : Tổng like bình luận
	/// IsLiked : Flag đánh đấu Current Member đã like chưa (check trong DB MemberId có ở trong List MemberLike hay không)
	/// </summary>
	public class Comment
	{
		public int CommentId { set; get; }
		public MemberInfo MemberComment { set; get; }
		public string Content { set; get; }
		public int TotalLike { set; get; }
		public bool IsLiked { set; get; }
	}
}
