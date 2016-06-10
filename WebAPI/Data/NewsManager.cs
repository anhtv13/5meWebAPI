using WebApi.CustomObject;
using Entity.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Log4net
{
    public class NewsManager
    {
        private static NewsManager m_instance;

        public static NewsManager Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new NewsManager();
                return m_instance;
            }
        }

        private Dictionary<string, DetailedNews> m_detailedNewsDict; //dictionary chứa thông tin các bài viết
        private Dictionary<string, Category> m_categoryList; //dictionary chứa thông tin hạng mục

        public NewsManager()
        {
            m_detailedNewsDict = new Dictionary<string, DetailedNews>();
            m_categoryList = new Dictionary<string, Category>();

#if DEBUG
            HardCodeData();
#endif
        }

        private void HardCodeData()
        {
            Category cat1 = new Category() { Code = "101", Name = "Food", Description = "An uong" };
            Category cat2 = new Category() { Code = "102", Name = "Cafe", Description = "Nhay mua" };
            Category cat3 = new Category() { Code = "103", Name = "Travel", Description = "Choi boi" };

            DetailedNews news1 = new DetailedNews("123", "Test1", null, "anhtv", "DongDa", "Hanoi", new Rate(20, 180),
                "Roquefort là một loại pho mát có mùi thơm, cay, nổi bật với những chấm xanh mốc nên không phải ai cũng thích. Tuy nhiên, những người mê loại pho mát này luôn khẳng định đây là pho mát xanh ngon nhất thế giới. \nLoại thực phẩm đặc biệt này được sản xuất tại ngôi làng nhỏ Roquefort-sur-Soulzon, trong một khu cao nguyên đá vôi ở Causse du Larzac, miền nam nước Pháp. Bên trong các hang động tối và ướt át với độ ẩm lên tới 90%, sữa cừu được chế biến cho chín tới và làm thành từng khuôn pho mát Roquefort theo phương pháp lưu giữ hàng thế kỷ qua. ",
                new List<string>() { "Fromage", "Roquefort", "Test" }, null, new List<string>() { cat1.Code }, null, DateTime.Now, true);

            m_categoryList.Add(cat1.Code, cat1); m_categoryList.Add(cat2.Code, cat2); m_categoryList.Add(cat3.Code, cat3);
            m_detailedNewsDict.Add(news1.ID, news1);
        }

        #region Action by Anonymouse User

        public object GetCategoryList()
        {
            List<Category> categoryList = new List<Category>(m_categoryList.Values);
            return categoryList;
        }

        /// <summary>
        /// return full news (detailed news) by id
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public object GetDetailedNewsById(string newsId)
        {
            //check in cache
            if (m_detailedNewsDict.ContainsKey(newsId))
            {
                if (m_detailedNewsDict[newsId].IsApproved)
                    return m_detailedNewsDict[newsId];
                ////check in database
                ////if exist return news, otherwise return null
                //throw new NotImplementedException();                
            }
            return new ResultObject(false, ErrorMessage.NotFound, null);
        }

        /// <summary>
        /// Return news list by category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public object GetNewsByCategory(string categoryId, int index, int offset, bool isAdmin)
        {
            List<DetailedNews> dNewsList = m_detailedNewsDict.Values.Where(t => t.CategoryList.Contains(categoryId) && t.IsApproved).ToList<DetailedNews>();
            if (isAdmin)
                dNewsList = m_detailedNewsDict.Values.Where(t => t.CategoryList.Contains(categoryId)).ToList<DetailedNews>();

            List<News> newsList = new List<News>();
            if (index > dNewsList.Count)
                return new ResultObject(false, ErrorMessage.NotFound, null);
            else
            {
                if (offset > dNewsList.Count)
                {
                    for (int i = index; i < dNewsList.Count; i++)
                    //newsList.Add(dNewsList[i]);
                    {
                        News n = GetNewsFromDetailedNews(dNewsList[i]);
                        newsList.Add(n);
                    }
                }
                else
                {
                    for (int i = index; i < offset; i++)
                    {
                        News n = GetNewsFromDetailedNews(dNewsList[i]);
                        newsList.Add(n);
                    }
                }
                return newsList;
            }
        }

        /// <summary>
        /// Get news by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public object GetNewsByTag(string tag, int index, int offset, bool isAdmin)
        {
            List<DetailedNews> dataList = new List<DetailedNews>();
            //m_detailedNewsDict.Values.Where(t => t.TagList.Contains(tag) && t.IsApproved).ToList();
            foreach (DetailedNews dnews in m_detailedNewsDict.Values)
            {
                foreach (string t in dnews.TagList)
                {
                    if (t.ToUpper() == tag.ToUpper())
                    {
                        dataList.Add(dnews);
                        break;
                    }
                }
            }

            if (isAdmin)
                dataList = m_detailedNewsDict.Values.Where(t => t.TagList.Contains(tag)).ToList();

            List<News> newsList = new List<News>();
            if (index > dataList.Count)
                return new ResultObject(false, ErrorMessage.NotFound, null);
            else
            {
                if (offset > dataList.Count)
                {
                    for (int i = index; i < dataList.Count; i++)
                    {
                        News n = GetNewsFromDetailedNews(dataList[i]);
                        newsList.Add(n);
                    }
                }
                else
                {
                    for (int i = index; i < offset; i++)
                    {
                        News n = GetNewsFromDetailedNews(dataList[i]);
                        newsList.Add(n);
                    }
                }
                return newsList;
                ////get from DB
                //List<News> dbList = new List<News>();

                ////merge 2 lists
                //cacheList.AddRange(dbList);
                //return cacheList;
            }
        }

        public object GetNewsByKeyword(string keyword, int index, int offset)
        {
            throw new NotImplementedException();
        }

        private News GetNewsFromDetailedNews(DetailedNews dNews)
        {
            News n = new News();
            n.ID = dNews.ID;
            n.CategoryList = dNews.CategoryList;
            n.Date = dNews.Date;
            n.Description = dNews.Description;
            n.ImageUrlList = dNews.ImageUrlList;
            n.Location = dNews.Location;
            n.Place = dNews.Place;
            n.Rate = dNews.Rate;
            n.WriterId = dNews.WriterId;
            return n;
        }
        #endregion

        #region Normal Writer
        /// <summary>
        /// Check user who call that endpoint is actually writer of the article
        /// if yes, allows user to edit
        /// otherwise, refuse to edit
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="newsid"></param>
        /// <returns></returns>
        public bool IsWriter(string newsid, string userid)
        {
            if (m_detailedNewsDict.Keys.Contains(newsid))
            {
                if (m_detailedNewsDict[newsid].WriterId == userid)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public object RequestUploadNews(CreateNews news, string userid)
        {
            news.WriterId = userid;
            //lưu vào cloud rồi lấy url, sau khi lấy url thì xóa List<Image> đi để giảm cache, ép kiểu ngược lại thành DetailedNews rồi cache

            //luu vao DB, tra lai newsID roi cap nhat vao cache
            throw new NotImplementedException();
        }

        public object RequestEditNews(DetailedNews news, string userid)
        {
            if (!m_detailedNewsDict.ContainsKey(news.ID))
            {
                return new ResultObject(false, "NewsID not found.", null);
            }

            //kiểm tra trong DB có ko, nếu có thì load vào cache

            if (IsWriter(news.ID, userid))
            {
                m_detailedNewsDict[news.ID].Description = news.Description;
                m_detailedNewsDict[news.ID].Content = news.Content;
                m_detailedNewsDict[news.ID].Location = news.Location;
                m_detailedNewsDict[news.ID].Place = news.Place;
                m_detailedNewsDict[news.ID].TagList = news.TagList;
                return m_detailedNewsDict[news.ID];
            }
            else
                return new ResultObject(false, ErrorMessage.Forbidden, null);
        }

        public object RequestDeleteNews(string newsId, string userid)
        {
            if (IsWriter(newsId, userid) || IsAdmin(userid))
            {
                //-xóa trong cache                
                m_detailedNewsDict.Remove(newsId);
                //-ko xóa trong DB, bản chất vẫn giữ lại nhưng đánh dấu disable trong DB
                //nếu trong Db ko tìm thấy trả về lỗi not found

                return new ResultObject(true, null, m_detailedNewsDict[newsId]);
            }
            return new ResultObject(false, ErrorMessage.Forbidden.ToString(), null);
        }

        public object DeleteCommentFromNews(string newsId, string userId, string commentId)
        {
            if (IsWriter(newsId, userId) || IsAdmin(userId))
            {
                //nếu có trong cache thì cập nhật
                if (m_detailedNewsDict.ContainsKey(newsId))
                {
                    foreach (Comment cmt in m_detailedNewsDict[newsId].CommentList)
                    {
                        if (cmt.ID == commentId)
                            m_detailedNewsDict[newsId].CommentList.Remove(cmt);
                    }
                }

                //luu vao DB                

                return new ResultObject(true, null, m_detailedNewsDict[newsId]);
            }
            else
                return new ResultObject(false, ErrorMessage.Forbidden.ToString(), null);
        }

        public object AddCommentToNews(string newsId, Comment cmt)
        {
            if (m_detailedNewsDict.ContainsKey(newsId))
            {
                int cmtCount = m_detailedNewsDict[newsId].CommentList.Count;
                cmt.ID = newsId + "_" + (cmtCount + 1); //set ID cho comment, convention = newsId_index (VD: bài viết id= 111, số cmt= 20 -> id cmt = 111_21)
                m_detailedNewsDict[newsId].CommentList.Add(cmt);
            }

            //luu vao DB
            throw new NotImplementedException();
        }

        public object NewsWrittenByWriter(string userId, int index, int offset)
        {
            List<News> newsList = m_detailedNewsDict.Values.Where(t => t.WriterId == userId).ToList<News>();
            return newsList;
        }
        #endregion

        #region Admin User
        public bool IsAdmin(string userid)
        {
            throw new NotImplementedException();
        }

        public object Admin_ApproveNews(string newsId, string userid, bool approve)
        {
            //-kiểm tra cache
            //-nếu chưa có kiểm tra DB
            //nếu ko tồn tại trong DB, trả lỗi
            //nếu tồn tại trong DB, add vào cache
            if (!m_detailedNewsDict.ContainsKey(userid))
            {
            }

            if (IsAdmin(userid))
            {
                m_detailedNewsDict[newsId].IsApproved = true;
                //lưu vào DB

                return new ResultObject(true, null, m_detailedNewsDict[newsId]);
            }

            else
                return new ResultObject(false, ErrorMessage.NotFound, null);
        }

        public object Admin_GetNewsByCategory(string categoryId, int index, int offset, string userid)
        {
            if (IsAdmin(userid))
                return GetNewsByCategory(categoryId, index, offset, true);
            else
                return GetNewsByCategory(categoryId, index, offset, false);
        }
        #endregion
    }
}
