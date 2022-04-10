using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WebClone.Models;

namespace WebClone.Services
{
    /// <summary>
    /// 网页处理服务工具
    /// </summary>
    public class WebPageService
    {
        private static string[] excludekeys = { "http:", "https:", "//", "#", "javascript:", "?", "tel:", "mailto:" };
        /// <summary>
        /// 获取所有html元素的href属性值,只获取站点本地的链接，站外的不获取
        /// </summary>
        /// <param name="html">页面的html源码</param>
        /// <returns></returns>
        public static List<UrlModel> GetLocalHrefs(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return new List<UrlModel>();

            Dictionary<string, UrlModel> urls = GetHrefs(url,html);
            List<UrlModel> newUrls = new List<UrlModel>();

            if (null != urls)
            {
                foreach (string key in urls.Keys)
                {
                    string newkey = key.ToLower();
                    bool iscontained = false;
                    foreach (var exkey in excludekeys)
                    {
                        if (newkey.IndexOf(exkey) == 0)
                        {
                            iscontained = true;
                            break;
                        }
                    }

                    if (!iscontained) {
                        //只获取本地路径
                        newUrls.Add(urls[key]);
                    }
                }
            }

            return newUrls;
        }

        /// <summary>
        /// 获取所有html元素的src属性值,只获取站点本地的链接，站外的不获取
        /// </summary>
        /// <param name="html">页面的html源码</param>
        /// <returns></returns>
        public static List<UrlModel> GetLocalSrcs(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return new List<UrlModel>();

            Dictionary<string, UrlModel> urls = GetSrc(url, html);
            List<UrlModel> newUrls = new List<UrlModel>();

            if (null != urls)
            {
                foreach (string key in urls.Keys)
                {
                    string newkey = key.ToLower();
                    bool iscontained = false;
                    foreach (var exkey in excludekeys)
                    {
                        if (newkey.IndexOf(exkey) == 0)
                        {
                            iscontained = true;
                            break;
                        }
                    }

                    if (!iscontained)
                    {
                        //只获取本地路径
                        newUrls.Add(urls[key]);
                    }
                }
            }

            return newUrls;
        }

        private static Dictionary<string, UrlModel> GetHrefs(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            UrlModel currUrl = UrlParser.Parse(url);
            Dictionary<string, UrlModel> urls = new Dictionary<string, UrlModel>();
            Regex reg = new Regex("href=\"(?<Url>.+?)\"", RegexOptions.IgnoreCase);
            
            if (currUrl != null)
            {
                AddUrlModel(html, currUrl, urls, reg);
            }

            return urls;
        }

        private static Dictionary<string, UrlModel> GetSrc(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            UrlModel currUrl = UrlParser.Parse(url);
            Dictionary<string, UrlModel> urls = new Dictionary<string, UrlModel>();
            Regex reg = new Regex("(src=\"(?<Url>.+?)\"|url\\((?<Url>.+?)\\))", RegexOptions.IgnoreCase);

            if (currUrl != null)
            {
                AddUrlModel(html, currUrl, urls, reg);
            }

            return urls;
        }

        private static void AddUrlModel(string html, UrlModel currUrl, Dictionary<string, UrlModel> urls, Regex reg)
        {
            if (reg.IsMatch(html))
            {
                MatchCollection matchs = reg.Matches(html);
                foreach (Match item in matchs)
                {
                    try
                    {
                        string strUrl = item.Groups["Url"].Value;
                        UrlModel model = new UrlModel();
                        model.RelatedPath = strUrl;
                        model.CurrPath = currUrl.CurrPath;
                        model.RootPath = currUrl.RootPath;
                        model.Scheme = currUrl.Scheme;
                        model.Port = currUrl.Port;
                        model.Host = currUrl.Host;

                        if (strUrl.StartsWith("/"))
                        {
                            //绝对目录情况下
                            model.AbsoluteUri = string.Format("{0}{1}", model.RootPath, model.RelatedPath);
                        }
                        else
                        {
                            //相对目录情况下
                            string currPath = model.CurrPath;
                            int depth = 0;
                            string path = model.RelatedPath;

                            if (path.StartsWith(".."))
                            {
                                try
                                {
                                    while (path.StartsWith(".."))
                                    {
                                        depth++;
                                        path = path.Substring(3);
                                        currPath = currPath.Substring(0, currPath.LastIndexOf("/"));
                                    }

                                    model.AbsoluteUri = string.Format("{0}/{1}", currPath, path);
                                }
                                catch
                                {

                                }
                            }
                            else
                            {
                                model.AbsoluteUri = string.Format("{0}/{1}", currPath, path);
                            }

                        }

                        strUrl = strUrl.Trim().ToLower();

                        urls.Add(strUrl, model);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
