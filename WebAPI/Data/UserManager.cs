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
        private Dictionary<string, string> m_tokenDict;//dictionary lưu thông tin authen (key:token string, value: userId)
        private Timer m_tokenTimer; //timer dùng để hủy token
        private object m_lock;
        private IptLogger m_log = new IptLogger("UserManager");

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
            m_tokenDict = new Dictionary<string, string>();
            m_tokenTimer = new Timer();
            m_tokenTimer.Interval = 30 * 60 * 1000; //half an hour
            m_tokenTimer.Elapsed += m_tokenTimer_Elapsed;
            m_lock = new object();

            HardCode();
        }

        private void HardCode()
        {
            User user = new User();
            user.Id = "1101040015";
            string password = "123456";
            user.Username = "anhtv";
            byte[] hashedPassword = HashedPassword.GetHash(password, user.Username);
            user.HashedPassword = hashedPassword;
            user.JoinedAt = DateTime.Now.AddDays(-1000);
            user.Role = 1;
            m_userDict.Add(user.Id, user);

            string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJTZXNzaW9uSWQiOiI1MmJjNWYyNS1hOTkzLTQ2ZTUtOGNlOS1hZDgyZmMwM2RlOTYiLCJDcmVhdGVBdCI6IlwvRGF0ZSgxNDY2NjQ5NTM0NzA4KVwvIiwiVXNlcm5hbWUiOiJhbmh0diIsIklkIjoiMTEwMTA0MDAxNSJ9.baAQn94lqjJhk1nhKhUQTHEH4_I3Ucs6O0ClPu9qrsgJv6IKR1SSEM1IRsxPyTQNRL_U-Fxb49Ab0QUWxhDu3JojGR-lD0eRbqeQeaSQLkjaF2GR1wgZ6mC4UI09PXNPIHJ4h5kbrJ-7RIOh4P7bUALccjSDiWHeceCXvyT4wINqGy8JVxPtvSLtt8Ab8ld3s-NyxECreGi-PQuwdfcg4RMlOyueH3TL8nF17T9ODHWzGalr6EVHJMeZPsJACt1QGHUXaMfDPr6Oq_8V-UYoFZDLe6V90YtkYA3trhw11vLMk_mOYnB7vmOfML27cpcKSHehJog_manVbPligvuxOA";
            m_tokenDict.Add(token, user.Id);
        }

        private void m_tokenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (m_lock)
                {
                    List<string> cacheList = new List<string>();
                    foreach (string token in m_tokenDict.Keys)
                    {
                        TokenObject tokenObj = JsonConvert.DeserializeObject<TokenObject>(token);
                        if ((tokenObj.CreateAt - DateTime.Now).Hours > 8)
                            cacheList.Add(token);
                    }
                    foreach (string cache in cacheList)
                        m_tokenDict.Remove(cache);
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.ToString());
            }
        }

        public string AuthenRequest(string authenObj)
        {
            AuthenRequest obj = JsonConvert.DeserializeObject<AuthenRequest>(authenObj);
            string username = obj.Username;
            string password = obj.Password;
            //hash password = key la username
            byte[] hashedPassword = HashedPassword.GetHash(password, username);

            ////kiểm tra trong cache, DB
            User user = null;
            foreach (User u in m_userDict.Values)
            {
                if (HashedPassword.CompareHash(password, u.HashedPassword, username))
                {
                    user = u;
                    break;
                }
            }
            if (user == null)
                throw new Exception(HttpStatusCode.Unauthorized.ToString());
            string token = CreateToken(user);
            return token;
        }

        //tạo token chứa thông tin cusid, username, sessionId, createAt
        private string CreateToken(User user)
        {
            Guid sessionId = Guid.NewGuid();
            TokenObject respond = new TokenObject();
            respond.Id = user.Id;
            respond.Username = user.Username;
            respond.SessionId = sessionId;
            respond.CreateAt = DateTime.Now;
            string token = TokenCreator.Instance.CreateToken(respond);
            m_tokenDict.Add(token, user.Id);
            //m_userDict.Add(user.Id, user);
            return token;
        }

        /// <summary>
        /// de client dang ki user moi, goi ham nay kiem tra username available hay ko
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckName(string username)
        {
            if (m_userDict.ContainsKey(username))
                return false;
            else
                return true;
        }

        public string RegisterNewUser(string userInfo)
        {
            RegisterUser newUser = JsonConvert.DeserializeObject<RegisterUser>(userInfo);
            if (!CheckName(userInfo) || newUser.Password.Length <= 6)
                throw new Exception("RegisterFailed");

            byte[] hashedPassword = HashedPassword.GetHash(newUser.Password, newUser.Username);
            User u = new User();
            u.Username = newUser.Username;
            u.HashedPassword = hashedPassword;
            u.Dob = newUser.Dob;
            u.JoinedAt = DateTime.Now;
            u.Role = 0;
            //luu vao DB, tra lai ID tu sinh

            m_userDict.Add(u.Id, u);
            string token = CreateToken(u);
            return token;
        }

        public bool IsAdmin(string token)
        {
            lock (m_lock)
            {
                if (m_tokenDict.ContainsKey(token))
                {
                    TokenObject obj = JsonConvert.DeserializeObject<TokenObject>(token);
                    string userId = obj.Id;
                    foreach (User u in m_userDict.Values)
                    {
                        if (u.Role == (int)UserRoles.Admin)
                            return true;
                    }
                }
                return false;
            }
        }

        public bool IsUser(string token)
        {
            lock (m_lock)
            {
                if (m_tokenDict.ContainsKey(token))
                    return true;
                else
                    return false;
            }
        }

        public bool CheckToken(string token)
        {
            lock (m_lock)
            {
                if (m_tokenDict.ContainsKey(token))
                    return true;
                else
                    return false;
            }
        }

        public string GetUserIdByToken(string token)
        {
            if (m_tokenDict.ContainsKey(token))
                return m_tokenDict[token];
            else
                return string.Empty;
        }
    }
}
