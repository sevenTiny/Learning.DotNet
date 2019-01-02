using System.Diagnostics;

namespace Test.SevenTiny.Bantina.Spring
{
    public interface IBusinessService
    {
        void Test();
        int GetInt();
        void TestVoid();
        void TestVoid2();
    }

    //[Interceptor]
    public class BusinessService : IBusinessService
    {
        [Action1]
        [Action2]
        public int GetInt()
        {
            return 1;
        }

        [Action1]
        [Action2]
        public void Test()
        {
            Trace.WriteLine("this is a test.");
        }

        public void TestVoid()
        {

        }

        [Action1]
        public void TestVoid2()
        {

        }
    }

}
