#if false
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

/// <summary>
/// 获取本机IP地址
/// </summary>
namespace ConsoleApplication1
{
    class 获取本机IP地址
    {
        public static List<string> GetLocalAddress(string netType)
        {
            string HostName = Dns.GetHostName();
            IPAddress[] address = Dns.GetHostAddresses(HostName);
            List<string> IpList = new List<string>();
            if (netType == string.Empty)
            {
                for (int i = 0; i < address.Length; i++)
                {
                    IpList.Add(address[i].ToString());
                }
            }
            else
            {
                for (int i = 0; i < address.Length; i++)
                {
                    if (address[i].AddressFamily.ToString() == netType)
                    {
                        IpList.Add(address[i].ToString());
                    }
                }
            }
            return IpList;
        }

        static void Main(string[] args)
        {
            // 获取IPV4地址
            List<string> ipv4 = GetLocalAddress("InterNetwork");
            foreach (string each in ipv4)
                Console.WriteLine(each);
            // 获取IPV6地址
            List<string> ipv6 = GetLocalAddress("InterNetworkV6");
            foreach (string each in ipv6)
                Console.WriteLine(each);
            Console.ReadKey();
        }
    }
}

#endif