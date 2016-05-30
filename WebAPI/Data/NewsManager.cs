using _5meProjects.CustomObject;
using Entity.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5meProjects.Data
{
    public class NewsManager
    {
        private Dictionary<string, DetailedNews> m_detailedNewsDict; //dictionary chứa thông tin các bài viết
        private Dictionary<string, NewsCategory> m_categoryList; //dictionary chứa thông tin hạng mục

        public NewsManager()
        {
            m_detailedNewsDict = new Dictionary<string, DetailedNews>();
            m_categoryList = new Dictionary<string, NewsCategory>();
        }

        #region Action by Anonymouse User
        /// <summary>
        /// return full news (detailed news) by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetailedNews GetDetailedNewsById(string id)
        {
            //check in cache
            if (m_detailedNewsDict.ContainsKey(id))
                return m_detailedNewsDict[id];
            else
            {
                //check in database
                //if exist return news, otherwise return null
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Return news list by category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<News> GetNewsByCategory(string categoryId, int index, int offset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get news by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<News> GetNewsByTag(string tag, int index, int offset)
        {
            //get from cache
            List<News> cacheList = new List<News>();
            foreach (DetailedNews detailedNews in m_detailedNewsDict.Values)
            {
                if (detailedNews.TagList.Contains(tag))
                {
                    cacheList.Add(detailedNews);
                }
            }
            //get from DB
            List<News> dbList = new List<News>();

            //merge 2 lists
            cacheList.AddRange(dbList);
            return cacheList;
        }

        public List<News> GetNewsByKeyword(string keyword, int index, int offset)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Normal User
        /// <summary>
        /// Check user who call that endpoint is actually writer of the article
        /// if yes, allows user to edit
        /// otherwise, refuse to edit
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="newsid"></param>
        /// <returns></returns>
        public bool IsWriter(string userid, string newsid)
        {
            if (m_detailedNewsDict.ContainsKey(newsid))
            {
                if (m_detailedNewsDict[newsid].Writer == userid)
                    return true;
            }
            return false;
        }

        public ResultObject RequestUploadNews(CreateNews news, string writerId)
        {
            news.Writer = writerId;
            //lưu vào cloud rồi lấy url, sau khi lấy url thì xóa List<Image> đi để giảm cache, ép kiểu ngược lại thành DetailedNews rồi cache

            //luu vao DB, tra lai newsID roi cap nhat vao cache
            throw new NotImplementedException();
        }

        public ResultObject RequestEditNews(DetailedNews news, string writer)
        {
            if (m_detailedNewsDict.ContainsKey(news.ID))
            {
                m_detailedNewsDict[news.ID].Description = news.Description;
                m_detailedNewsDict[news.ID].Content = news.Content;
                m_detailedNewsDict[news.ID].Location = news.Location;
                m_detailedNewsDict[news.ID].Place = news.Place;
                m_detailedNewsDict[news.ID].TagList = news.TagList;
            }

            //kiểm tra id của bài viết có trong DB chưa, có thì sửa các rows, ko có thì báo lỗi
            return new ResultObject(true, null);
        }

        public ResultObject RequestDeleteNews(string newsId)
        {
            //-xóa trong cache
            //-ko xóa trong DB, bản chất vẫn giữ lại nhưng đánh dấu disable trong DB
            m_detailedNewsDict.Remove(newsId);

            return new ResultObject(true, null);
        }        

        public ResultObject DeleteCommentFromNews(string newsId, string commentId)
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
            throw new NotImplementedException();

            return new ResultObject(true, null);
        }

        public ResultObject AddCommentToNews(string newsId, Comment cmt)
        {
            if (m_detailedNewsDict.ContainsKey(newsId))
            {
                int cmtCount = m_detailedNewsDict[newsId].CommentList.Count;
                cmt.ID = newsId + "_" + (cmtCount + 1); //set ID cho comment, convention = newsId_index (VD: bài viết id =111, số cmt= 20 -> id cmt = 111_21)
                m_detailedNewsDict[newsId].CommentList.Add(cmt);
            }

            //luu vao DB
            throw new NotImplementedException();

            return new ResultObject(true, null);
        }
        #endregion
    }
}
