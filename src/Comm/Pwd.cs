using System;
using System.Security.Cryptography;
using System.Text;

namespace Preoff.Comm
{
    public class Pwd
    {
        public static string Ecoding(string pwd)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                string strResult = BitConverter.ToString(result);
                return strResult.Replace("-", ""); 
            }
        }
    }
}
