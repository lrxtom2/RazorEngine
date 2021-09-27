using RazorEngine;
using RazorEngine.Templating;
using System;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string template = "Hello @Model.Name, welcome to RazorEngine!";
            var result = Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });
            Console.WriteLine("Hello World!");
        }
    }
}