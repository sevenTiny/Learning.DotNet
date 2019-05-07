using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace StandardCL.Reflection
{
    public class PropertyOperate
    {
        public void SetPrivateProperty()
        {
            TestClass instance = new TestClass();
            instance.Age = 111;
            instance.Name = "111";

            PropertyInfo field = instance.GetType().GetProperty("PrivateName", BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(instance, "12312123", null);
        }
    }
}
