using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using StandardCL.RoslynScript;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Test.Standard.RoslynScript
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

        [Fact]
        [Trait("desc", "检测脚本")]
        public void Compilation()
        {
            string code1 = @"
using System;
            public class ScriptedClass
            {
                public string HelloWorld { get; set; }
                public ScriptedClass()dd
                {
                    HelloWorld = ""Hello Roslyn!"";
                }
            }";
            var syntaxTree = CSharpSyntaxTree.ParseText(code1);

            // 指定编译选项。
            var assemblyName = $"GenericGenerator.g";
            var compilation = CSharpCompilation.Create(assemblyName, new[] { syntaxTree },
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddReferences(AppDomain.CurrentDomain.GetAssemblies().Select(t =>
                    {
                        try
                        {
                            return t.Location;
                        }
                        catch (Exception)
                        {
                            return string.Empty;
                        }
                    }).Where(t => !string.IsNullOrEmpty(t)).Select(x => MetadataReference.CreateFromFile(x)));

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);

                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                }
            }
        }

        [Fact]
        [Trait("desc", "命名空间的使用")]
        public void CallScriptFromTextWithUsing()
        {
            //#r ""nuget: Newtonsoft.Json, 12.0.1""
            string code1 = @"

            using System.Collections.Generic;
            using Newtonsoft.Json;

            public class ScriptedClass
            {
                public static string GetIntList()
                {
                    var list = new List<int>();
                    for (int i = 0; i < 10; i++)
                    {
                        list.Add(i);
                    }
                    return JsonConvert.SerializeObject(list);
                }
            }";

            var script = CSharpScript.RunAsync(code1,
                ScriptOptions.Default.AddReferences("Newtonsoft.Json")//引用dll
                ).Result;

            var result = script.ContinueWithAsync<string>("ScriptedClass.GetIntList()").Result;
        }

        [Fact]
        [Trait("desc", "命名空间的使用")]
        public void CallScriptFromTextWithUsingUseCreate()
        {
            string code1 = @"
            using System.Collections.Generic;
            using Newtonsoft.Json;

            public class ScriptedClass
            {
                public static string GetIntList()
                {
                    var list = new List<int>();
                    for (int i = 0; i < 10; i++)
                    {
                        list.Add(i);
                    }
                    return JsonConvert.SerializeObject(list);
                }
            }

            return ScriptedClass.GetIntList();
            ";

            var script = CSharpScript.Create<string>(code1,
                ScriptOptions.Default.AddReferences("Newtonsoft.Json")//引用dll
                );

            script.Compile();

            var result = script.RunAsync().Result.ReturnValue;
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

            var script = CSharpScript.RunAsync(code1, globals: new Arg { arg1 = name }, globalsType: typeof(Arg)).Result;

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

        [Fact]
        public void Syntax()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
 
namespace TopLevel
{
    using Microsoft;
    using System.ComponentModel;
 
    namespace Child1
    {
        using Microsoft.Win32;
        using System.Runtime.InteropServices;
 
        class Foo { }
    }
 eqweq
    namespace Child2
    {
        using System.CodeDom;
        using Microsoft.CSharp;
 
        class Bar { }
    }
}");

            var root = (CompilationUnitSyntax)tree.GetRoot();
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
