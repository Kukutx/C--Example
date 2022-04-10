#if false
using System;
using System.Collections;
using System.Threading;
/// <summary>
/// 线程操作基础
/// </summary>
namespace ConsoleApplication1
{
    class Program
    {
        // 定义一个无参线程函数
        public static void My_Thread()
        {
            Console.WriteLine("线程函数已运行");
            Thread.Sleep(1000);
        }

        static void Main(string[] args)
        {
            string strinfo = string.Empty;
            ThreadStart childref = new ThreadStart(My_Thread);
            Thread thread = new Thread(childref);

            thread.Start();

            Console.WriteLine("是否为后台线程: " + thread.IsBackground);
            Console.WriteLine("线程唯一标识符: " + thread.ManagedThreadId);
            Console.WriteLine("线程优先级: " + thread.Priority.ToString());
            Console.WriteLine("线程名称: " + thread.Name);
            Console.WriteLine("线程状态: " + thread.ThreadState.ToString());

            Thread.Sleep(1000);
            thread.Join();
            Console.ReadKey();
        }
    }
}

#endif