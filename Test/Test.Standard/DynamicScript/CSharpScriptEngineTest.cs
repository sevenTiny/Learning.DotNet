using Microsoft.CodeAnalysis.Scripting;
using StandardCL.DynamicScript;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Standard.DynamicScript
{
    public class CSharpScriptEngineTest
    {
        [Fact]
        [Trait("desc", "调用脚本方法")]
        public void CallScriptFromText()
        {
            string code1 = @"
            public class ScriptedClass
            {
                public string HelloWorld { get; set; }
                public ScriptedClass()
                {
                    HelloWorld = ""Hello Roslyn!"";
                }
            }";

            string code2 = ("new ScriptedClass().HelloWorld");

            var codes = new string[] { code1, code2 };

            var result = CSharpScriptEngine.Run(codes);

            Assert.Equal("Hello Roslyn!", result.ToString());
        }

        [Fact]
        [Trait("desc", "使用类的实例调用类的方法，并获取返回值")]
        public void CallScriptFromAssembly()
        {
            var result = CSharpScriptEngine.Run("return new TestClass().GetString();"
                , ScriptOptions.Default
                .WithReferences(typeof(TestClass).Assembly)
                .WithImports("Test.Standard.DynamicScript"));

            Assert.Equal("hello world！", result.ToString());
        }
    }

    public class TestClass
    {
        public string GetString()
        {
            return "hello world！";
        }
    }
}
