using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Shared;

namespace Entity.News
{
	/// <summary>
	/// Entity News Info
	/// Thông tin sơ lược 1 bài post, để hiển thị trên trang chủ hoặc search
	/// 
	/// PostId : Id bài post
	/// Description : Mô tả của bài viết
	/// Image : Hình ảnh bài viết
	/// PostMember : Thông tin người đăng
	/// Place : Địa điểm
	/// Location : Địa danh
	/// Rate : Thông tin đánh giá
	/// </summary>
	public class NewsInfo
	{
		public int PostId { set; get; }
		public string Description { set; get; }
		public Image Image { set; get; }
		public MemberInfo PostMember { set; get; }
		public string Place { set; get; }
		public string Location { set; get; }
		public Rate Rate { set; get; }
	}

	/// <summary>
	/// Collection của NewsInfo, hiển thị trên trang chủ kèm Category
	/// </summary>
	public class NewsInfoCollection
	{
		public Category Category { set; get; }
		public List<NewsInfo> Collection { set; get; }
	}
}
