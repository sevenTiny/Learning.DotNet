using LF_Async.Code;
using System;
using System.Threading;
using Xunit;

namespace LF_Async
{
    public class UnitTest1
    {
        [Trait("desc","async异步流方法执行顺序")]
        [Fact]
        public void AsyncExecuteSequnce()
        {
            new AsyncExecuteSequnce().ExecuteNoReturn();
            Thread.Sleep(2000);
        }

        [Trait("desc", "async异步流方法执行顺序")]
        [Fact]
        public void AsyncExecuteReturnSequnce()
        {
            new AsyncExecuteSequnce().ExecuteReturn();
            Thread.Sleep(2000);
        }

        [Trait("desc", "Task方法执行顺序")]
        [Fact]
        public void TaskExecuteSequnce()
        {
            new TaskExecuteSequnce().ExecuteNoReturn();
            Thread.Sleep(2000);
        }
    }
}
