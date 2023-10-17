using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper
{
    public static class HUser
    {
        public static string ConvertToMD5(this string clearText)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(clearText);
            MD5 mD5 = MD5.Create();
            byte[] HashData = mD5.ComputeHash(byteData);

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < HashData.Length; i++)
            {
                stringBuilder.Append(HashData[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
