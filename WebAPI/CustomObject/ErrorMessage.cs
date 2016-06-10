using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.CustomObject
{
    public class ErrorMessage
    {
        //server
        public static string ServerOverloaded = "ServerOverloaded";
        public static string InternalServerError = "InternalServerError";

        //Authentication
        public static string Unauthorized = "Unauthorized";
        public static string InvalidToken = "InvalidToken";
        public static string TokenExpired = "TokenExpired";
        public static string Forbidden = "Forbidden";
        public static string AuthenFailed = "AuthenFailed";

        //Data
        public static string NotFound = "NotFound";

        //Request
        public static string BadRequest = "BadRequest";
    }
}
