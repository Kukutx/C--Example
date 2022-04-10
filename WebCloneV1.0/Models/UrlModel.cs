/*
 * 作者：jonlan
 * 时间：2018-8-25
 * 邮箱：547294770@qq.com
 * */
using System;
using System.Collections.Generic;
using System.Text;

namespace WebClone.Models
{
    public class UrlModel
    {
        public string RelatedPath { get; set; }
        public string AbsoluteUri { get; set; }
        public string CurrPath { get; set; }
        public string RootPath { get; set; }

        public string Host { get; set; }
        public int Port { get; set; }
        public string Scheme { get; set; }
    }
}
