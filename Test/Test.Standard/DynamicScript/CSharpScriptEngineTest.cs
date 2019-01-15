using Microsoft.CodeAnalysis.CSharp.Scripting;
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
        [Trait("desc", "调用动态创建的脚本方法")]
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

            var script = CSharpScript.RunAsync(code1).Result;

            var result = script.ContinueWithAsync<string>("new ScriptedClass().HelloWorld").Result;

            Assert.Equal("Hello Roslyn!", result.ReturnValue);
        }

        [Trait("desc", "调用动态创建的带参数的脚本方法")]
        [Theory]
        [InlineData("123")]
        public void CallScriptFromTextWithArguments(string name)
        {
            string code1 = @"
            public class ScriptedClass
            {
                public string GetString(string name)
                {
                    return name;
                }
            }
            return new ScriptedClass().GetString(arg1);      
            ";

            var script = CSharpScript.RunAsync(code1,globals:new Arg { arg1 = name }, globalsType: typeof(Arg)).Result;

            var result = script.ReturnValue;

            Assert.Equal(name, result);
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

        [Trait("desc", "使用类的实例调用类的带参数的方法，并获取返回值")]
        [Theory]
        [InlineData("123")]
        public void CallScriptFromAssemblyWithArgument(string x)
        {
            var script = CSharpScript.Create<string>("return new TestClass().DealString(arg1);",
                ScriptOptions.Default
                .WithReferences(typeof(TestClass).Assembly)
                .WithImports("Test.Standard.DynamicScript"), globalsType: typeof(TestClass));

            script.Compile();

            var result = script.RunAsync(new TestClass { arg1 = x }).Result.ReturnValue;

            Assert.Equal(x, result.ToString());
        }
    }

    public class TestClass
    {
        public string arg1 { get; set; }

        public string GetString()
        {
            return "hello world！";
        }

        public string DealString(string a)
        {
            return a;
        }
    }

    public class Arg
    {
        public string arg1 { get; set; }
    }
}
