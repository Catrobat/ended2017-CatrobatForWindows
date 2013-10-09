using System;
using System.Text;

namespace Catrobat.Core.Utilities
{
    public static class UtilTokenHelper
    {
        public static string ToHex(byte[] array)
        {
            var hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static string CalculateToken(String username, string password)
        {
            var tmp = MD5Core.GetHash(Encoding.UTF8.GetBytes(username.ToLower()));
            var md5Username = ToHex(tmp).ToLower();

            tmp = MD5Core.GetHash(Encoding.UTF8.GetBytes(password));
            var md5Password = ToHex(tmp).ToLower();

            tmp = MD5Core.GetHash(Encoding.UTF8.GetBytes(md5Username + ":" + md5Password));
            var token = ToHex(tmp);

            return token;
        }
    }
}