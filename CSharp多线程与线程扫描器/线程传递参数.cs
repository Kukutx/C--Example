#if false
/// <summary>
/// 线程传递参数
/// </summary>
namespace ConsoleApplication1
{
    public struct ThreadObj
    {
        public string name;
        public int age;

        public ThreadObj(string _name, int _age)
        {
            this.name = _name;
            this.age = _age;
        }
    }

    class Program
    {
        // 定义一个无参线程函数
        public static void My_Thread(object obj)
        {
            ThreadObj thread_path = (ThreadObj)obj;
            Console.WriteLine("姓名: {0} 年纪: {1}", thread_path.name, thread_path.age);
            Thread.Sleep(3000);
        }
        static void Main(string[] args)
        {
            for (int x = 0; x < 200; x++)
            {
                ThreadObj obj = new ThreadObj("admin", x);
                Thread thread = new Thread(My_Thread);
                thread.IsBackground = true;
                thread.Start(obj);
            }
            Console.ReadKey();
        }
    }
}

#endif