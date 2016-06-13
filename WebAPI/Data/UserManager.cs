using Entity.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WebApi.CustomObject;
using WebApi.Secure;

namespace WebApi.Log4net
{
    public class UserManager
    {
        private Dictionary<string, User> m_userDict; //dictionary lưu thông tin của user (key:userid, value: object user)
        private Dictionary<string, List<TokenObject>> m_tokenDict;//dictionary lưu thông tin authen (key:userid, value: Obj Token)
        private Timer m_tokenTimer; //timer dùng để hủy token
        private object m_lock;

        private static UserManager m_Instance;

        public static UserManager Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new UserManager();
                return m_Instance;
            }
        }

        private UserManager()
        {
            m_userDict = new Dictionary<string, User>();
            m_tokenDict = new Dictionary<string, List<TokenObject>>();
            m_tokenTimer = new Timer();
            m_tokenTimer.Interval = 30 * 60 * 1000; //half an hour
            m_tokenTimer.Elapsed += m_tokenTimer_Elapsed;
            m_lock = new object();

            HardCode();
        }

        private void HardCode()
        {
            User user = new User();
            string password = "123456";
            user.Id = "anhtv123";
            user.Username = "anhtv";
            user.JoinedAt = DateTime.Now.AddDays(-1000);
            byte[] hashedPassword = Crypto.GetHash(password, user.Username);
            user.HashedPassword = hashedPassword;

            m_userDict.Add(user.Id, user);
        }

        private void m_tokenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (m_lock)
            {
                foreach (List<TokenObject> tokenList in m_tokenDict.Values)
                {
                    ////nếu user object chứa biến token
                    //List<string> cacheList = new List<string>();
                    //foreach (string token in m_userDict[userId].TokenList)
                    //{
                    //    string json = Jose.JWT.Decode(token, SecureHelper.Instance.PrivateKey());
                    //    dynamic obj = JsonConvert.DeserializeObject(json);
                    //    DateTime dt = obj.CreateAt;
                    //    if ((DateTime.Now - dt).Hours >= 8)
                    //        cacheList.Add(json);
                    //}

                    //foreach (string s in cacheList)
                    //    m_userDict[userId].TokenList.Remove(s);

                    List<TokenObject> cacheList = new List<TokenObject>();
                    foreach (TokenObject tokenObj in tokenList)
                    {
                        if ((tokenObj.CreateAt - DateTime.Now).Hours > 8)
                            cacheList.Add(tokenObj);
                    }
                    foreach (TokenObject cache in cacheList)
                        tokenList.Remove(cache);
                }
            }
        }

        public object AuthenRequest(object authenObj)
        {
            AuthenRequest obj = JsonConvert.DeserializeObject<AuthenRequest>(authenObj.ToString());
            string username = obj.Username;
            string password = obj.Password;
            //hash password = key la username
            byte[] hashedPassword = Crypto.GetHash(password, username);

            ////kiểm tra trong cache, DB
            User user = null;
            foreach (User u in m_userDict.Values)
            {
                if (Crypto.CompareHash(password, u.HashedPassword, username))
                {
                    user = u;
                    break;
                }
            }
            if (user == null)
                throw new Exception(ErrorMessage.AuthenFailed);

            //tạo token chứa thông tin cusid, username, sessionId, createAt
            Guid sessionId = Guid.NewGuid();
            TokenObject respond = new TokenObject();
            respond.Id = user.Id;
            respond.Username = user.Username;
            respond.SessionId = sessionId;
            respond.CreateAt = DateTime.Now;
            string token = SecureHelper.Instance.CreateToken(respond);
            if (!m_tokenDict.ContainsKey(user.Id))
                m_tokenDict.Add(user.Id, new List<TokenObject>());
            m_tokenDict[user.Id].Add(respond);
            return token;
        }
    }
}
