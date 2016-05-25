using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.News
{
	/// <summary>
	/// Entity Create News
	/// Sử dụng khi tạo bài viết, blog hoặc post
	/// 
	/// PostMemberId : Id member viết bài
	/// Description : Mô tả của bài viết, hiển thị nếu content không có caption
	/// Content : Nội dung bài viết (Format và type chưa rõ)
	/// IsPost : Là bài post hay blog
	/// Place : Địa điểm
	/// Location : Địa danh
	/// Tags : List tag
	/// </summary>
	public class CreateNews
	{
		public int PostMemberId { set; get; }
		public string Description { set; get; }
		public string Content { set; get; }
		public bool IsPost { set; get; }
		public string Place { set; get; }
		public string Location { set; get; }
		public List<string> Tags { set; get; }
	}
}
