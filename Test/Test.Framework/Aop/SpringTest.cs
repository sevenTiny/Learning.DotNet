using Microsoft.VisualStudio.TestTools.UnitTesting;
using SevenTiny.Bantina.Spring.Aop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.SevenTiny.Bantina.Spring
{
    [TestClass]
    public class SpringTest
    {
        static SpringTest()
        {
            new StartUp().Start();
        }

        [TestMethod]
        public void Test()
        {
            var instance = DynamicProxy.CreateProxyOfRealize<IBusinessService, BusinessService>();

            instance.GetInt();

            instance.TestVoid2();

            instance.Test();
        }
    }

    public class BusinessServiceProxy : IBusinessService
    {
        //private ServiceProvider _serviceProvider = new ServiceProvider();

        private BusinessService _serviceImpObj;

        public BusinessServiceProxy()
        {
           // this._serviceImpObj = (BusinessService)this._serviceProvider.GetService("Test.Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Test.SevenTiny.Bantina.Spring.BusinessService");
        }

        public int GetInt()
        {
            Action1 action = new Action1();
            Action2 action2 = new Action2();
            string method = "GetInt";
            object[] parameters = new object[0];
            action.Before(method, parameters);
            action2.Before(method, parameters);
            object obj = this._serviceImpObj.GetInt();
            obj = action2.After(method, obj);
            obj = action.After(method, obj);
            return (int)obj;
        }

        public void Test()
        {
            Action1 action = new Action1();
            Action2 action2 = new Action2();
            string method = "Test";
            object[] parameters = new object[0];
            action.Before(method, parameters);
            action2.Before(method, parameters);
            this._serviceImpObj.Test();
            object result = null;
            action2.After(method, result);
            action.After(method, result);
        }

        public void TestVoid()
        {
            this._serviceImpObj.TestVoid();
        }

        public void TestVoid2()
        {
            Action1 action = new Action1();
            string method = "TestVoid2";
            object[] parameters = new object[0];
            action.Before(method, parameters);
            this._serviceImpObj.TestVoid2();
            object result = null;
            action.After(method, result);
        }
    }
}
