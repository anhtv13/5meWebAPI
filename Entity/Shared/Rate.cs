using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Shared
{
	/// <summary>
	/// Entity Rate
	/// Đánh giá
	/// 
	/// RateValue : Giá trị đánh giá
	/// TotalRate : Tổng số người đánh giá
	/// IsRated : Flag đánh dấu Current Member đã rate chưa
	/// </summary>
	public class Rate
	{
		public float RateValue { set; get; }
		public int TotalRate { set; get; }
		public bool IsRated { set; get; }
	}
}
