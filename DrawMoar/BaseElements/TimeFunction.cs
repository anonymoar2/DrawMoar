using System;

using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;


namespace DrawMoar.BaseElements
{
    public delegate double Del(double x);
    public class TimeFunction
    {
        private string timeFunc;

        private string begin = @"using System;
namespace MyNamespace
{
    public delegate double Del(double x);
    public static class LambdaCreator 
    {
        public static Del Create()
        {
            return (x)=>";
        private string end = @";
        }
    }
}";

        public TimeFunction(string timeFunc) {
            this.timeFunc = timeFunc;
        }


        public double GetTime(double time) {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            parameters.ReferencedAssemblies.Add("System.dll");
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, begin + Normalize(timeFunc) + end);
            var cls = results.CompiledAssembly.GetType("MyNamespace.LambdaCreator");
            var method = cls.GetMethod("Create", BindingFlags.Static | BindingFlags.Public);
            var del = (method.Invoke(null, null) as Delegate);
            return Convert.ToDouble(del.DynamicInvoke(time));
        }


        public string Normalize(string input) {
            string result = input;

            result = Regex.Replace(result, "T, t", "Math.Tan()");
            result = Regex.Replace(result, "[C, c]os", "Math.Cos");
            result = Regex.Replace(result, "[S, s]in", "Math.Sin");

            result = Regex.Replace(result, "[A, a]bs", "Math.Abs");
            result = Regex.Replace(result, "[S, s]qrt", "Math.Sqrt");
            result = Regex.Replace(result, "[L, l]og", "Math.Log");
            result = Regex.Replace(result, "[L, l]og10", "Math.Log10");
            result = Regex.Replace(result, "[E, e]xp", "Math.Exp");

            result = Regex.Replace(result, "[p,P][i,I]", "Math.PI");
            result = Regex.Replace(result, "[E, e]", "Math.E");

            result = Regex.Replace(result, @"(\d+)(x)", @"$1*$2");
            result = Regex.Replace(result, @"(\(?\d*x\)?)\^(\d+)", "Math.Pow($1,$2)");

            return result;
        }
    }
}
