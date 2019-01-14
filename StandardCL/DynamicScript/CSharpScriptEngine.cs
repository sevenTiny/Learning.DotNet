using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StandardCL.DynamicScript
{
    /// <summary>
    /// Roslyn脚本引擎
    /// </summary>
    public class CSharpScriptEngine
    {
        public static object Evaluate(string code, ScriptOptions scriptOptions = null)
        {
            return CSharpScript.EvaluateAsync(code, scriptOptions ?? ScriptOptions.Default).Result;
        }
        
        public static object Run(string code, ScriptOptions scriptOptions = null)
        {
            ScriptState<object> scriptState = CSharpScript.RunAsync(code, scriptOptions ?? ScriptOptions.Default).Result;
            return scriptState.ReturnValue;
        }

        public static object Run(IEnumerable<string> codes, ScriptOptions scriptOptions = null)
        {
            ScriptState<object> scriptState = null;
            foreach (var code in codes.ToArray())
            {
                scriptState = scriptState == null ? CSharpScript.RunAsync(code, scriptOptions ?? ScriptOptions.Default).Result : scriptState.ContinueWithAsync(code, scriptOptions ?? ScriptOptions.Default).Result;
            }
            return scriptState.ReturnValue;
        }
    }
}
