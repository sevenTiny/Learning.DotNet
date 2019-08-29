using StandardCL.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Standard.Reflection
{
    public class PropertyOperateTest
    {
        [Fact]
        public void SetProperty()
        {
            new PropertyOperate().SetPrivateProperty();
        }
    }
}
