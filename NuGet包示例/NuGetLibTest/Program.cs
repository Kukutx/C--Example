using NuGetLib;
using System;
//引用项目测试
namespace NuGetLibTest {
    internal class Program {
        private static void Main(string[] args)
        {
            Console.WriteLine($"1 + 1 = {NuGetLibClass.Add(1, 1)}");
            Console.ReadLine();
        }
    }
}