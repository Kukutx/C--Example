using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WebClone.Models;

namespace WebClone.Services
{
    public class UrlSevices
    {
        public static Dictionary<string, UrlModel> GetHrefs(string html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            Dictionary<string, UrlModel> urls = new Dictionary<string, UrlModel>();
            Regex reg = new Regex("href=\"(?<Url>.+?)\"", RegexOptions.IgnoreCase);

            if (reg.IsMatch(html))
            {
                MatchCollection matchs = reg.Matches(html);
                foreach (Match item in matchs)
                {
                    string url = item.Groups["Url"].Value;
                    UrlModel model = new UrlModel();
                    model.Url = url;
                    model.IsAccess = false;
                    url = url.Trim().ToLower();
                    
                    try
                    {
                        urls.Add(url, model);
                    }
                    catch
                    {
                    }
                }
            }

            return urls;
        }

        public static Dictionary<string, UrlModel> GetSrc(string html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            Dictionary<string, UrlModel> urls = new Dictionary<string, UrlModel>();
            Regex reg = new Regex("(src=\"(?<Url>.+?)\"|url\\((?<Url>.+?)\\))", RegexOptions.IgnoreCase);

            if (reg.IsMatch(html))
            {
                MatchCollection matchs = reg.Matches(html);
                foreach (Match item in matchs)
                {
                    string url = item.Groups["Url"].Value;
                    UrlModel model = new UrlModel();
                    model.Url = url;
                    model.IsAccess = false;

                    url = url.Trim().ToLower();

                    try
                    {
                        urls.Add(url, model);
                    }
                    catch
                    {
                    }
                }
            }

            return urls;
        }
    }
}
