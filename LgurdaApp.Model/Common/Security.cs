using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoughFormApp
{
    class Security
    {

        //private static readonly UserDTO userDTO = UserDTO.Instance;

        //Used for AesEncryption and AesDecryption
        static private string g_mac_key = "aujxw8k4mf9tlv0!";
        static private string g_fixed_key = "@wf8y6t_*4zkjd78";
        //Used for AesEncryption and AesDecryption
        static private byte[] iv16Bit = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        //used as MAC SessionKey
        static private byte[] secretkey = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        static private byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

        private const string g_save_user_pwd1 = "0922837750";
        private const string g_save_user_pwd2 = "1474132735";


        public static string AesEncrypt(string dataToEncrypt, byte[] keyX)
        {
            // byte[] KeyByte = System.Text.Encoding.Unicode.GetBytes(g_fixed_key);

            var bytes = Encoding.Default.GetBytes(dataToEncrypt);
            using (var aes = new AesCryptoServiceProvider())
            {
                using (var ms = new MemoryStream())
                using (var encryptor = aes.CreateEncryptor(keyX, iv16Bit))
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    var cipher = ms.ToArray();
                    return Convert.ToBase64String(cipher);
                }
            }

        }

        public static string AesDecrypt(string dataToDecrypt, byte[] keyX)
        {
            // key = System.Text.Encoding.Unicode.GetBytes(g_fixed_key);

            var bytes = Convert.FromBase64String(dataToDecrypt);
            using (var aes = new AesCryptoServiceProvider())
            {
                using (var ms = new MemoryStream())
                using (var decryptor = aes.CreateDecryptor(keyX, iv16Bit))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    var cipher = ms.ToArray();
                    // return Encoding.UTF8.GetString(cipher);
                    string sad = Encoding.UTF8.GetString(cipher);
                    return sad;
                }
            }
        }

        public static Guid CreateMac(string dataToBeHashed)
        {
            //MAC Key is converted to byte
            byte[] gMacKey = System.Text.Encoding.Unicode.GetBytes(g_mac_key);

            byte[] data = Encoding.Unicode.GetBytes(dataToBeHashed);
            HMACMD5 hmacMD5 = new HMACMD5(gMacKey);
            byte[] macSender = hmacMD5.ComputeHash(data);
            Guid guid = new Guid(macSender);
            return guid;
        }

        public static bool IsValidMac(Guid originalMac, string dataToBeHashed)
        {
            return (CreateMac(dataToBeHashed) == originalMac);
        }

        private static string RandomStringGenerate()
        {
            string input = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*|";
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 16; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }

        //This Method is changed for firstly getting random generated code and then encryption 
        //Previous method signature
        //public static string GetEncryptedText(string PlainText)

        public static string GetEncryptedText()
        {
            //Random key
            string randomKeyString = RandomStringGenerate();
            byte[] randomkey = new byte[32];
            randomkey = Encoding.Unicode.GetBytes(randomKeyString);

            //Fixed key
            Byte[] fixedKey = new Byte[32];
            fixedKey = Encoding.Unicode.GetBytes(g_fixed_key);

            string eText = AesEncrypt(CreateRandomCode(8), randomkey);
            string eKey = AesEncrypt(randomKeyString, fixedKey);
            string encryptValue = eKey + eText;
            return encryptValue;

        }

        public static string GetEncryptedText(string encryptedText)
        {
            //Random key
            string randomKeyString = RandomStringGenerate();
            byte[] randomkey = new byte[32];
            randomkey = Encoding.Unicode.GetBytes(randomKeyString);

            //Fixed key
            Byte[] fixedKey = new Byte[32];
            fixedKey = Encoding.Unicode.GetBytes(g_fixed_key);

            string eText = AesEncrypt(encryptedText, randomkey);
            string eKey = AesEncrypt(randomKeyString, fixedKey);
            string encryptValue = eKey + eText;
            return encryptValue;

        }

        public static string GetPlainText(string encryptedPassword)
        {
            string eKey = encryptedPassword.Substring(0, 44);
            string eText = encryptedPassword.Substring(44, encryptedPassword.Length - 44);

            Byte[] fixedKey = new Byte[32];
            fixedKey = Encoding.Unicode.GetBytes(g_fixed_key);

            string plainPassword = AesDecrypt(eKey, fixedKey);
            byte[] decryptedRandomkey = Encoding.Unicode.GetBytes(plainPassword);

            string plainText = AesDecrypt(eText, decryptedRandomkey);
            return plainText;

        }

        public static string CreateRandomCode(int codeCount)
        {

            string allChar = "2,3,4,5,6,7,8,9,a,A,b,B,C,d,D,e,E,f,F,g,G,h,H,J,K,L,m,M,n,N,P,Q,r,R,S,t,T,U,V,W,X,Y,Z";

            string[] allCharArray = allChar.Split(',');

            string randomCode = string.Empty;

            int temp = -1;

            Random rand = new Random();

            for (int i = 0; i < codeCount; i++)
            {

                if (temp != -1)
                {

                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));

                }

                int t = rand.Next(36);

                if (temp != -1 && temp == t)
                {

                    return CreateRandomCode(codeCount);

                }

                temp = t;

                randomCode += allCharArray[t];

            }

            return randomCode;

        }

    }
}
