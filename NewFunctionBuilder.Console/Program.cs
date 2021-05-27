
namespace NewFunctionBuilder.Console
{
    using NewFunctionBuilder.Logic;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string text = "4 * (6 - 2) + 5";
            Console.WriteLine(new Calculate().ToCalculate(new RPN().Parse(text)));
        }
    }
}
