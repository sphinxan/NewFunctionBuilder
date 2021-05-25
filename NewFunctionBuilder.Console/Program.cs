
namespace NewFunctionBuilder.Console
{
    using NewFunctionBuilder.Logic;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string text = "4 * (6 - 2) + 5";
            var rpn = new RPN();
            var calc = new Calculate();
            var elements = rpn.Parse(text);

            Console.WriteLine(calc.ToCalculate(elements));
        }
    }
}
