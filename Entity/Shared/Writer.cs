using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.News
{
	/// <summary>
	/// Entity MemberInfo
	/// Thông tin người đăng bài
	/// 
	/// MemberId : Id người đăng
	/// Name : Tên
	/// AvatarUrl : Link ảnh đại diện
	/// Url : Link đến profile
	/// </summary>
	public class Writer
	{
		public int MemberId { set; get; }
		public string Name { set; get; }
		public string AvatarUrl { set; get; }
		public string Url { set; get; }

        public Writer() { }
	}    
}
