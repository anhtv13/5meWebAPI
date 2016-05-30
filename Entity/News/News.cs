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
	/// ID : Id bài post (dù lưu trong DB là int tự tăng, nhưng trong enity nên dùng string)
	/// Description : Mô tả của bài viết
	/// Image : Hình ảnh bài viết
    /// Writer : ID người đăng
	/// Place : Địa điểm
	/// Location : Địa danh
	/// Rate : Thông tin đánh giá
	/// </summary>
	public class News
	{
		public string ID { set; get; }
		public string Description { set; get; }
		public List<string> ImageUrlList { set; get; }
		public string Writer { set; get; }
		public string Place { set; get; }
		public string Location { set; get; }
		public Rate Rate { set; get; }
        public List<Category> CategoryList { set; get; }

        public News(string id, string description, List<string> imgUrlList, string writer, string place, string location, Rate rate, List<Category> categoryList)
        {
            ID = id;
            Description = description;
            ImageUrlList = imgUrlList;
            Writer = writer;
            Place = place;
            Location = location;
            Rate = rate;
            CategoryList = categoryList;
        }

        public News() { }
	}
}
