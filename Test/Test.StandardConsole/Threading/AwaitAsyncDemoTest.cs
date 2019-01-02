using StandardCL.Threading;

namespace Test.StandardConsole.Threading
{
    public class AwaitAsyncDemoTest
    {
        public void Main()
        {
            var awaitAsyncDemo = new AwaitAsyncDemo();
            awaitAsyncDemo.Main();
        }

        public void MainAsync()
        {
            var awaitAsyncDemo = new AwaitAsyncDemo();
            awaitAsyncDemo.MainAsync();
        }
    }
}
