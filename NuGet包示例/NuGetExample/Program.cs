using NuGetLib;
using System;
//这里是引用NuGet包测试
namespace NuGetLibTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"1 + 1 = {NuGetLibClass.Add(1, 1)}");
            Console.ReadLine();
        }
    }
}