using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RSATest
{
    /// <summary>
    /// http://www.csharpdeveloping.net/Snippet/how_to_encrypt_decrypt_using_asymmetric_algorithm_rsa
    /// </summary>
    public class AsymEncryptDecrypt
    {
        public static int keySize = 1024;
        public static string publicKey = "<RSAKeyValue><Modulus>7lcr1ILIjrKf5oIi4oCYz8ajD4lOUn4Z2OnxKbdl2a4NnOjKbhm+Xboxp6gFdFxf3HloVhegp8c2IvOpsEpV0yinx1yyDMQEXrbkPsrsqrlBsR2s0IWDcQ4pdRk5RPWNJpFAFaP6Jis8cUl+hh8O2SHok0DTbdSKN6BRcv7cE0U=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string privateKey = "<RSAKeyValue><Modulus>7lcr1ILIjrKf5oIi4oCYz8ajD4lOUn4Z2OnxKbdl2a4NnOjKbhm+Xboxp6gFdFxf3HloVhegp8c2IvOpsEpV0yinx1yyDMQEXrbkPsrsqrlBsR2s0IWDcQ4pdRk5RPWNJpFAFaP6Jis8cUl+hh8O2SHok0DTbdSKN6BRcv7cE0U=</Modulus><Exponent>AQAB</Exponent><P>7rq2QHo3sgsPZmis26hPvRZU5Rm36CE8mR7ydN96v9dkWUO6JN6et05DrcTa0jWJuhGFgwoL5JHPVbFyEG8GuQ==</P><Q>/5VCE2E7CBj3aQmHa1gHZRCLmwdibYoZuflIaR/xt6ybYV2bGMmOVFrmAVRmpasOgEQixyuSBsGk4OiJLHyq7Q==</Q><DP>gJM9D3MDxjegvrZHyzJWZ++8H3v/id1Exu9dBEdM1EAMwurwOWVRNEbJurBYbnj5SaagMthZWWySr4OXfHRfuQ==</DP><DQ>kGkfip+3oR2qPdk6gPaeRwaQOypy/P25B5HIzk1UYLnQmbUwO1M3boZly36K+TSr3uGg3PTLb7HenY4GY/KVCQ==</DQ><InverseQ>kfG1cNp1NhihwU66z80i4o+nH4TR/HxW6N0IBO3MS66mYhi+vjMm2URib99LSJBAxTe8X/SmAhZ0CrmUe5C+tA==</InverseQ><D>FH0KX8WRW5D35W8c9Bcla8ER3eFKChXcf3TW3iTtnLmVYn5xfmb4oiKCJ70uINtbAz8tjK8RyLJ7kSQcm3K8b1TCXSIPnqTINA5Jk6VVELd93t6YNsRkoaGgoL6cIaPuAfhzo6r4E4AXH1HisDTl6T7u9oNWPBv6cPB6zNDvatk=</D></RSAKeyValue>";


        private static bool _optimalAsymmetricEncryptionPadding = false;

        //public static void GenerateKeys(int keySize, out string publicKey, out string publicAndPrivateKey)
        //{
        //    using (var provider = new RSACryptoServiceProvider(keySize))
        //    {
        //        publicKey = provider.ToXmlString(false);
        //        publicAndPrivateKey = provider.ToXmlString(true);
        //    }
        //}

        public static string EncryptText(string text, int keySize, string publicKeyXml)
        {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), keySize, publicKeyXml);
            return Convert.ToBase64String(encrypted);
        }

        public static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            int maxLength = GetMaxDataLength(keySize);
            if (data.Length > maxLength) throw new ArgumentException(String.Format("Maximum data length is {0}", maxLength), "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }

        public static string DecryptText(string text, int keySize, string publicAndPrivateKeyXml)
        {
            var decrypted = Decrypt(Convert.FromBase64String(text), keySize, publicAndPrivateKeyXml);
            return Encoding.UTF8.GetString(decrypted);
        }

        public static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicAndPrivateKeyXml)) throw new ArgumentException("Key is null or empty", "publicAndPrivateKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicAndPrivateKeyXml);
                return provider.Decrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }

        public static int GetMaxDataLength(int keySize)
        {
            if (_optimalAsymmetricEncryptionPadding)
            {
                return ((keySize - 384) / 8) + 7;
            }
            return ((keySize - 384) / 8) + 37;
        }

        public static bool IsKeySizeValid(int keySize)
        {
            return keySize >= 384 &&
                    keySize <= 16384 &&
                    keySize % 8 == 0;
        }
    }
}
