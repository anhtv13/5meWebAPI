using Jose;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Secure
{
    public class TokenCreator
    {
        private static TokenCreator m_secureHelper;

        public static TokenCreator Instance
        {
            get
            {
                if (m_secureHelper == null)
                    m_secureHelper = new TokenCreator();
                return m_secureHelper;
            }
        }

        private X509Certificate2 m_certificate;

        private TokenCreator()
        {
            InitSecureKey();
        }

        private void InitSecureKey()
        {
            //reference find path of a file in project:
            //http://stackoverflow.com/questions/12335618/file-path-for-project-files
            Stream rsaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebApi.Secure.certificate.p12");
            var bytes = new byte[rsaStream.Length];
            rsaStream.Read(bytes, 0, bytes.Length);
            m_certificate = new X509Certificate2(bytes, "1", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
        }

        public RSACryptoServiceProvider PrivateKey()
        {
            var key = (RSACryptoServiceProvider)m_certificate.PrivateKey;

            RSACryptoServiceProvider newKey = new RSACryptoServiceProvider();
            newKey.ImportParameters(key.ExportParameters(true));

            return newKey;
        }

        private RSACryptoServiceProvider PublicKey()
        {
            return (RSACryptoServiceProvider)m_certificate.PublicKey.Key;
        }

        public string CreateToken(object data)
        {
            string token = JWT.Encode(data, PrivateKey(), JwsAlgorithm.RS256);
            return token;
        }

        public bool VerifySignature(string token)
        {
            try
            {
                string json = Jose.JWT.Decode(token, PrivateKey());//decode + verify signature
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
