using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LF_Async.Code
{
    /// <summary>
    /// Task 异步执行顺序
    /// 日志标明了代码执行的顺序
    /// </summary>
    public class TaskExecuteSequnce
    {
        public void ExecuteNoReturn()
        {
            Trace.WriteLine(1);

            DoSthAsync();

            Trace.WriteLine(4);
        }

        private void DoSthAsync()
        {
            Trace.WriteLine(2);

            //Task 只是将执行逻辑放到了异步线程，然后继续走主线程逻辑

            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Trace.WriteLine(5);
            });

            Trace.WriteLine(3);
        }
    }
}
