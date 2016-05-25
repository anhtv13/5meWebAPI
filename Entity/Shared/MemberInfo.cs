using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Shared
{
	/// <summary>
	/// Entity MemberInfo
	/// Thông tin người đăng bài
	/// 
	/// MemberId : Id người đăng
	/// Name : Tên
	/// Avatar : Link ảnh đại diện
	/// Url : Link đến profile
	/// </summary>
	public class MemberInfo
	{
		public int MemberId { set; get; }
		public string Name { set; get; }
		public Image Avatar { set; get; }
		public string Url { set; get; }
	}
}
