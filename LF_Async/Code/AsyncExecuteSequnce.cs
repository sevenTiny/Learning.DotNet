using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LF_Async.Code
{
    /// <summary>
    /// async 异步执行顺序
    /// 日志标明了代码执行的顺序
    /// </summary>
    public class AsyncExecuteSequnce
    {
        #region 不带返回值
        public void ExecuteNoReturn()
        {
            Trace.WriteLine(1);

            //这里会进入异步方法，遇到await，立即结束该方法
            _ = DoSthAsync();

            //异步方法执行到await，会立即结束异步方法，而继续执行主方法的下一行逻辑，也就是下面代码
            Trace.WriteLine(3);
        }

        private async Task DoSthAsync()
        {
            Trace.WriteLine(2);

            /*
                这里只要遇到了await，就会立即结束该方法，而不执行后续的逻辑
                编译器将后面的逻辑包装成回调函数在await方法执行完毕以后继续执行回调函数（也就是await后面的逻辑）
             */
            await Task.Run(() => {
                Thread.Sleep(2000);
                Trace.WriteLine(4);
            });

            Trace.WriteLine(5);
        }
        #endregion

        #region 带返回值
        public void ExecuteReturn()
        {
            Trace.WriteLine(1);

            //这里会进入异步方法，遇到await，不会结束该方法，而是阻塞等待
            var re = DoSthAsyncReturn();

            //这里如果不用Result的话，和没有返回值的异步走的同样的回调
            //但是有Result的话，会阻塞等待结果
            Trace.WriteLine($"5 {re.Result}");

            Trace.WriteLine(6);
        }

        private async Task<string> DoSthAsyncReturn()
        {
            Trace.WriteLine(2);

            /*
                该方法由于在外面使用Result等待结果，因此同步执行了方法
             */
            await Task.Run(() => {
                Thread.Sleep(2000);
                Trace.WriteLine(3);
            });

            Trace.WriteLine(4);

            return "return value";
        }
        #endregion
    }
}
