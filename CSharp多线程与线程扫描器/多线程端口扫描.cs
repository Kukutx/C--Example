#if false
using System.Net;
using System.Net.Sockets;
/// <summary>
/// 多线程端口扫描
/// </summary>
namespace TimeoutPortScan
{
    class TimeoutPortScan
    {
        private IPAddress ip;
        private readonly int[] ports = new int[] { 21, 22, 23, 25, 53, 80, 110, 118, 135, 143, 156, 161,
            443, 445, 465, 587, 666, 990, 991, 993, 995, 1080, 1433, 1434, 1984, 2049, 2483, 2484, 3128,
            3306, 3389, 4662, 4672, 5222, 5223, 5269, 5432, 5500, 5800, 5900, 8000, 8008, 8080 };

        public bool Connect(IPEndPoint remoteEndPoint, int timeoutMSec)
        {
            Socket scanSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                IAsyncResult result = scanSocket.BeginConnect(remoteEndPoint, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeoutMSec), false);
                if (result.IsCompleted && scanSocket.Connected)
                {
                    scanSocket.EndConnect(result);
                    return true;
                }
                else
                    return false;
            }
            finally
            {
                scanSocket.Close();
            }
        }

        static void Main(string[] args)
        {
            TimeoutPortScan ps = new TimeoutPortScan();
            for (int x = 1; x < 255; x++)
            {
                string addr = string.Format("192.168.43.{0}", x);
                IPAddress.TryParse(addr, out ps.ip);
                for (int num = 0; num < ps.ports.Length; num++)
                {
                    if (ps.Connect(new IPEndPoint(ps.ip, ps.ports[num]), 100))
                        Console.WriteLine("IP:{0} --> 端口: {1} --> 状态: Open", addr, ps.ports[num]);
                }
            }
        }
    }
}

#endif