using System;
using System.Collections.Generic;
using System.Linq;

namespace NewFunctionBuilder.Logic
{
    public class Calculate
    {
        private char[] rpnParse;
        private bool argument;
        private double xMin, xMax, step;

        readonly char[] Symbols = new char[] { '+', 'x', '-', '*', '/', '^' };
        
        public Calculate(string expression, double xMin, double xMax, double step)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.step = step;

            rpnParse = RPN.Parse(expression);
                            
            argument = rpnParse.Any(x => x != ' '); //Проверка на существование хотя бы одного элемента в последовательности
        }

        public string RpnStr(string expression)
        {
            char[] functionList = RPN.Parse(expression).ToArray();
            string str = "";
            for (var i = 0; i < functionList.Length; i++)
            {
                str += functionList[i];
            }

            return str;
        }

        public Dictionary<double, double> FunctionValues()
        {
            Dictionary<double, double> result = new Dictionary<double, double>();

            double element = xMin;
            do
            {
                result.Add(element, ToCalculate(element));
                element += step;
            }
            while (argument && (xMax - element) >= 0);

            return result;
        }

        public double ToCalculate(double element)
        {
            var Answer = new Stack<double>();

            foreach (var item in rpnParse)
            {
                //если число, кладем в стек
                if (!Symbols.Contains(item))
                {
                    Answer.Push(double.Parse(item.ToString()));
                }

                else if (item.ToString() == "x")
                {
                    Answer.Push(element);
                }

                //если операция
                else
                {
                    //извлекаем из стека 2 числа, результат кладем в стек
                    var operationResult = DoOperation(item, Answer.Pop(), Answer.Pop());
                    Answer.Push(operationResult);
                }
            }
            //последнее число в стеке - ответ
            return Answer.Pop();
        }

        private double DoOperation(char op, double firstArg, double secondArg)
        {
            switch (op)
            {
                case '+':
                    return secondArg + firstArg;
                case '-':
                    return secondArg - firstArg;
                case '*':
                    return secondArg * firstArg;
                case '/':
                    {
                        if (firstArg != 0.0)
                            return secondArg / firstArg;
                        else
                            throw new Exception("Ошибка. Деление на ноль");
                    }
                case '^':
                    return Math.Pow(secondArg, firstArg);
                default:
                    throw new Exception("Такого оператора нет");
            }
        }
    }
}
