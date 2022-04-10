using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace AsyncPortScan
{
    class AsyncPortScan
    {
        static void Main(string[] args)
        {
            IPAddress ip;
            int startPort, endPort;
            if (GetPortRange(args, out ip, out startPort, out endPort) == true)  // 提取命令行参数
                Scan(ip, startPort, endPort);   // 端口扫描
        }

        /// 从命令行参数中提取端口
        private static bool GetPortRange(string[] args, out IPAddress ip, out int startPort, out int endPort)
        {
            ip = null;
            startPort = endPort = 0;
            // 帮助命令
            if (args.Length != 0 && (args[0] == "/?" || args[0] == "/h" || args[0] == "/help"))
            {
                Console.WriteLine("scan 192.168.1.10 100 2000");
                return false;
            }

            if (args.Length == 3)
            {
                // 解析端口号成功
                if (IPAddress.TryParse(args[0], out ip) && int.TryParse(args[1], out startPort) && int.TryParse(args[2], out endPort))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        /// 端口 扫描
        static void Scan(IPAddress ip, int startPort, int endPort)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int port = startPort; port < endPort; port++)
            {
                Socket scanSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                //寻找一个未使用的端口进行绑定
                do
                {
                    try
                    {
                        scanSocket.Bind(new IPEndPoint(IPAddress.Any, rand.Next(65535)));
                        break;
                    }
                    catch
                    {
                        //绑定失败
                    }
                } while (true);

                try
                {
                    scanSocket.BeginConnect(new IPEndPoint(ip, port), ScanCallBack, new ArrayList() { scanSocket, port });
                }
                catch
                {
                    continue;
                }
            }
        }

        /// BeginConnect的回调函数 异步Connect的结果
        static void ScanCallBack(IAsyncResult result)
        {
            // 解析 回调函数输入 参数
            ArrayList arrList = (ArrayList)result.AsyncState;
            Socket scanSocket = (Socket)arrList[0];
            int port = (int)arrList[1];
            // 判断端口是否开放
            if (result.IsCompleted && scanSocket.Connected)
            {
                Console.WriteLine("端口: {0,5} 状态: Open", port);
            }
            scanSocket.Close();
        }
    }
}
