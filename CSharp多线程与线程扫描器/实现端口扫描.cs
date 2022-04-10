#if false
using System.Net.Sockets;
/// <summary>
/// 实现端口扫描
/// </summary>
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // FTP, SSH, Telnet, SMTP, HTTP, POP3, RPC, SMB, SMTP, IMAP, POP3
            int[] Port = new int[] { 21, 22, 23, 25, 80, 110, 135, 445, 587, 993, 995 };

            foreach (int each in Port)
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                try
                {
                    sock.Connect("192.168.1.10", each);
                    if (sock.Connected)
                    {
                        Console.WriteLine("端口开启:" + each);
                    }
                }
                catch
                {
                    Console.WriteLine("端口关闭:" + each);
                    sock.Close();
                }
            }
        }
    }
}

#endif