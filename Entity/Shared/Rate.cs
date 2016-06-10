using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.News
{
    /// <summary>
    /// Entity Rate
    /// Đánh giá
    /// 
    /// RateNum : Tổng số người đánh giá
    /// TotalRate : Giá trị đánh giá
    /// IsRated : Flag đánh dấu Current Member đã rate chưa
    /// </summary>
    public class Rate
    {
        public float RateNum { set; get; }
        public float TotalRate { set; get; }
        public bool IsRated { set; get; }

        public Rate(float rateNum, float totalRate, bool isRated = false)
        {
            RateNum = rateNum;
            TotalRate = totalRate;
            IsRated = isRated;
        }

        public Rate() { }
    }
}
