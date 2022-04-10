using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WebClone.Models;

namespace WebClone.Services
{
    public class UrlParser
    {
        public static UrlModel Parse(string url)
        {
            UrlModel model = new UrlModel();

            //默认
            if (url.Length < 8)
                throw new Exception("url参数不正确");
            else if (!url.ToLower().StartsWith("http:") && !url.ToLower().StartsWith("https:"))
                throw new Exception("url格式有误");

            if (url.LastIndexOf('/') < 8)
                url = url + "/";

            Regex reg = new Regex("(?<scheme>(http|https))://(?<host>.+?)/", RegexOptions.Singleline);

            if (reg.IsMatch(url))
            {
                string scheme = reg.Match(url).Groups["scheme"].Value;
                string host = reg.Match(url).Groups["host"].Value;
                if (host.Contains(":"))
                {
                    var aa = host.Split(':');
                    if (aa.Length == 2)
                    {
                        model.Host = aa[0];
                        model.Port = int.Parse(aa[1]);
                    }
                }
                else
                {
                    model.Host = host;
                    model.Port = 80;
                }

                int index = url.IndexOf('/', 8);

                model.RelatedPath = url.Substring(index);
                model.AbsoluteUri = url;
                model.Scheme = scheme;
                model.CurrPath = url.Substring(0, url.LastIndexOf("/"));

                if (80 == model.Port)
                {
                    model.RootPath = string.Format("{0}://{1}", model.Scheme, model.Host);
                }
                else
                {
                    model.RootPath = string.Format("{0}://{1}:{2", model.Scheme, model.Host, model.Port);
                }
            }
            else
            {
                throw new Exception("url解析失败!");
            }

            return model;
        }
    }
}
