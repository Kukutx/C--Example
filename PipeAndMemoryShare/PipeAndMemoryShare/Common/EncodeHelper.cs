using System.IO;
using System.Security.Cryptography;

namespace Common
{
    /// <summary>
    /// 编码帮助类
    /// </summary>
    public static class EncodeHelper
    {
        /// <summary>
        /// 文件32位MD5
        /// </summary>
        /// <param name="filePath">待计算文件</param>
        /// <returns></returns>
        public static string CalcFileMD5(string filePath)
        {
            if (!File.Exists(filePath)) { return ""; }

            string md5 = "";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] data = MD5.Create().ComputeHash(fileStream);

                for (int i = 0; i < data.Length; i++)
                {
                    md5 += data[i].ToString("X");
                }
            }

            return md5;
        }
    }
}
