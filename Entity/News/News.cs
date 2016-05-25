﻿using System;
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
	/// 
	/// PostId : Id bài post
	/// Description : Mô tả của bài viết, hiển thị nếu content không có caption
	/// Content : Nội dung bài viết (Format và type chưa rõ)
	/// PostMember : Thông tin người đăng bài
	/// Place : Địa điểm
	/// Location : Địa danh
	/// Rate : Thông tin đánh giá
	/// Tags : List tag
	/// Comments : List bình luận bài viết
	/// </summary>
	public class News
	{
		public int PostId { set; get; }
		public string Description { set; get; }
		public string Content { set; get; }
		public MemberInfo PostMember { set; get; }
		public string Place { set; get; }
		public string Location { set; get; }
		public Rate Rate { set; get; }
		public List<string> Tags { set; get; }
		public List<Comment> Comments { set; get; }
	}
}
