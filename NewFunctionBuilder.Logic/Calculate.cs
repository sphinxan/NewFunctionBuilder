using System;
using System.Collections.Generic;
using System.Linq;

namespace NewFunctionBuilder.Logic
{
    public class Calculate
    {
        readonly char[] Symbols = new char[] { '+', '-', '*', '/', '^' };

        public double ToCalculate(char[] elements)
        {
            var answer = new Stack<double>();

            foreach (var item in elements)
            {
                //если число, кладем в стек
                if (!Symbols.Contains(item))
                {
                    answer.Push(double.Parse(item.ToString()));
                }

                /*else if (item.ToString() == "x")
                { }*/

                //если операция
                else
                {
                    //извлекаем из стека числа 2 раза
                    //(кол-во аргументов у операции в моем случае пока что всегда 2)
                    var arguments = new double[2];
                    for (int i = 2; i > 0; i--)
                    {
                        //числа в стеке лежат в обратном порядке, разворачиваем их
                        arguments[i - 1] = answer.Pop();
                    }
                    var operationResult = DoOperation(item, arguments);
                    //результат кладем в стек
                    answer.Push(operationResult);
                }
            }
            //последнее число в стеке - ответ
            return answer.Pop();
        }

        private double DoOperation(char op, double[] arguments)
        {
            //(double)arguments[0] + (double)arguments[1];
            switch (op)
            {
                case '+':
                    return arguments[0] + arguments[1];
                case '-':
                    return arguments[0] - arguments[1];
                case '*':
                    return arguments[0] * arguments[1];
                case '/':
                    return arguments[0] / arguments[1];
                case '^':
                    return Math.Pow(arguments[0], arguments[1]);
                default:
                    throw new Exception("Такого оператора нет");
            }
        }
    }
}
