using Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Entity.News
{
    /// <summary>
    /// Entity Create News
    /// Sử dụng khi tạo bài viết, blog hoặc post
    /// 
    /// Nhận từ client Object Image
    /// </summary>
    public class CreateNews : DetailedNews
    {
        public List<Image> ImageList { set; get; }
    }
}
