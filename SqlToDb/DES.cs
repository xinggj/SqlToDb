using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SqlToDb
{

    class DES
    {
        private static byte[] keyArray=null;
        public static byte[] KeyArray
        {
            set { keyArray = value; }
        }
       public static string Encrypt(string toEncrypt)            {                              byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);                   RijndaelManaged rDel = new RijndaelManaged();              rDel.Key = keyArray;                rDel.Mode = CipherMode.ECB;               rDel.Padding = PaddingMode.PKCS7;                  ICryptoTransform cTransform = rDel.CreateEncryptor();                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);                   return Convert.ToBase64String(resultArray, 0, resultArray.Length);            }
       public static string Decrypt(string toDecrypt)
       {
           byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

           RijndaelManaged rDel = new RijndaelManaged();
           rDel.Key = keyArray;
           rDel.Mode = CipherMode.ECB;
           rDel.Padding = PaddingMode.PKCS7;

           ICryptoTransform cTransform = rDel.CreateDecryptor();
           byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

           return UTF8Encoding.UTF8.GetString(resultArray);
       }   
    }
}
