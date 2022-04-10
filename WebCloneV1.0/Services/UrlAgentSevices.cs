using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WebClone.Models;

namespace WebClone.Services
{
    public class UrlAgentSevices
    {
        public static List<UrlModel> HandleHrefs(string html)
        {
            Dictionary<string,UrlModel> urls = UrlSevices.GetHrefs(html);
            List<UrlModel> newUrls = new List<UrlModel>();

            foreach (string key in urls.Keys)
            {
                string newkey = key.ToLower();
                if (!newkey.StartsWith("http:")
                    && !newkey.StartsWith("//")
                    && !newkey.StartsWith("https:"))
                {
                    newUrls.Add(urls[key]);
                }
            }

            return newUrls;
        }

        public static List<UrlModel> HandleSrcs(string html)
        {
            Dictionary<string, UrlModel> urls = UrlSevices.GetSrc(html);
            List<UrlModel> newUrls = new List<UrlModel>();

            foreach (string key in urls.Keys)
            {
                string newkey = key.ToLower();
                if (!newkey.StartsWith("http:") 
                    && !newkey.StartsWith("//") 
                    && !newkey.StartsWith("https:"))
                {
                   newUrls.Add(urls[key]);
                }
            }

            return newUrls;
        }
    }
}
