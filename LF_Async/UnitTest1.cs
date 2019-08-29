using LF_Async.Code;
using System;
using System.Threading;
using Xunit;

namespace LF_Async
{
    public class UnitTest1
    {
        [Trait("desc","async�첽������ִ��˳��")]
        [Fact]
        public void ExecuteSequnce()
        {
            new AsyncExecuteSequnce().ExecuteNoReturn();
            Thread.Sleep(2000);
        }

        [Trait("desc", "async�첽������ִ��˳��")]
        [Fact]
        public void ExecuteReturnSequnce()
        {
            new AsyncExecuteSequnce().ExecuteReturn();
            Thread.Sleep(2000);
        }
    }
}
