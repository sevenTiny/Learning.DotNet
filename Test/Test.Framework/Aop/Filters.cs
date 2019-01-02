using SevenTiny.Bantina.Spring.Aop;
using System.Diagnostics;

namespace Test.SevenTiny.Bantina.Spring
{
    public class InterceptorAttribute : InterceptorBaseAttribute
    {
        public override object Invoke(object @object, string method, object[] parameters)
        {
            Trace.WriteLine("Invoke");
            return base.Invoke(@object, method, parameters);
        }
    }

    public class Action1 : ActionBaseAttribute
    {
        public override object After(string method, object result)
        {
            Trace.WriteLine("Action After 111");
            return base.After(method, result);
        }

        public override void Before(string method, object[] parameters)
        {
            Trace.WriteLine("Action Before 111");
            base.Before(method, parameters);
        }
    }

    public class Action2 : ActionBaseAttribute
    {
        public override object After(string method, object result)
        {
            Trace.WriteLine("Action After 222");
            return base.After(method, result);
        }

        public override void Before(string method, object[] parameters)
        {
            Trace.WriteLine("Action Before 222");
            base.Before(method, parameters);
        }
    }
}
