using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebClone.Tools
{
    public class HttpTool
    {
        public static string HttpGet(string url, string referer, string encoding, out string msg)
        {
            msg = string.Empty;
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                //request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Referer = referer;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36";
                //request.Headers.Add("Accept-Language", "zh-cn");
                //request.Headers.Add("Accept-Encoding", "gzip,deflate");

                request.Timeout = 60000;//一分钟

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding(encoding));
                    result = reader.ReadToEnd();
                    reader.Close();
                    responseStream.Close();
                    request.Abort();
                    response.Close();
                    return result.Trim();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
            }

            return result;
        }

        public static void DownFile(string uRLAddress, string localPath, string filename)
        {
            WebClient client = new WebClient();
            Stream str = client.OpenRead(uRLAddress);
            StreamReader reader = new StreamReader(str);
            byte[] mbyte = new byte[1000000];
            int allmybyte = (int)mbyte.Length;
            int startmbyte = 0;

            while (allmybyte > 0)
            {
                int m = str.Read(mbyte, startmbyte, allmybyte);
                if (m == 0)
                {
                    break;
                }
                startmbyte += m;
                allmybyte -= m;
            }

            reader.Dispose();
            str.Dispose();

            string path = Path.Combine(localPath, filename);
            FileStream fstr = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fstr.Write(mbyte, 0, startmbyte);
            fstr.Flush();
            fstr.Close();
        }
    }
}
