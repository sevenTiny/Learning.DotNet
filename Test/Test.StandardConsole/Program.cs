using System;
using System.Threading;
using Test.StandardConsole.Threading;

namespace Test.StandardConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new AwaitAsyncDemoTest().MainAsync();

            Thread.CurrentThread.Join();

            Console.WriteLine("any key to exit ...");
            Console.Read();
        }
    }
}
