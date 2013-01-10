using System;
using System.Text;

namespace Catrobat.Core.Misc
{
  public class Utils
  {
    public static string toHex(byte[] array)
    {
      StringBuilder hex = new StringBuilder(array.Length * 2);
      foreach (byte b in array)
        hex.AppendFormat("{0:x2}", b);
      return hex.ToString();
    }

    public static string calculateToken(String username, string password)
    {
      byte[] tmp = MD5Core.GetHash(Encoding.UTF8.GetBytes(username.ToLower()));
      string md5Username = toHex(tmp).ToLower();

      tmp = MD5Core.GetHash(Encoding.UTF8.GetBytes(password));
      string md5Password = toHex(tmp).ToLower();

      tmp = MD5Core.GetHash(Encoding.UTF8.GetBytes(md5Username + ":" + md5Password));
      string token = toHex(tmp);

      return token;
    }
  }
}
