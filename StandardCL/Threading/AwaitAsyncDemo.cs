using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace StandardCL.Threading
{
    public class AwaitAsyncDemo
    {
        public void Main()
        {
            Trace.WriteLine("main start...");
            var str = GetString();
            Trace.WriteLine(str.Result);
            Trace.WriteLine("main end !");
        }

        public async void MainAsync()
        {
            Trace.WriteLine("main start...");

            Task<string> strTask = GetString();

            Trace.WriteLine("main middle ...");

            string str = await strTask;

            Trace.WriteLine("str = " +  str);

            Trace.WriteLine("main end !");
        }

        private async Task<string> GetString()
        {
            return await Task.Run(() => "this is a string");
        }
    }
}
