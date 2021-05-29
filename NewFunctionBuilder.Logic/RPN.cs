using System.Collections.Generic;
using System.Linq;

namespace NewFunctionBuilder.Logic
{
    public class RPN
    {
        public static readonly char[] elements;

        public static char[] Parse(string text)                                                         //Stack.Push() Вставляет элемент в верх Stack(помещаем в стек)
        {                                                                                               //Stack.Pop() Удаляет элемент из верхней части Stack
            char[] expression = ParseExpression(text);                                                  //Stack.Peek() Возвращает элемент, расположенный в верхней части Stack, но не удаляет его                                         
            Queue<char> tokens = ToRPN(expression);                                                     //Queue.Enqueue() Добавляет элемент в конец Queue
            var elements = tokens.ToArray();                                                            //Queue.Dequeue() удаляет самый старый элемент из начала Queue
                                                                                                        //Queue.Peek() Возвращает самый старый элемент, находящийся в начале Queue, но не удаляет его
            return elements;
        }

        private static char[] ParseExpression(string text)
        {
            return text.Replace(" ", "").ToArray();
        }

        private static Queue<char> ToRPN(char[] expression)
        {
            var operations = new Stack<char>();
            var operands = new Queue<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                var token = expression[i];

                if (CheckNumber(token.ToString())) //Если число/переменная, помещаем в очередь
                {
                    operands.Enqueue(token);
                }

                else
                {
                    //Если '(', или стек пуст, или приоритет символов < приоритета token, то помещаем token в стек
                    if (operations.Count() == 0 || token.ToString() == "(" || GetPriority(operations.Peek()) < GetPriority(token))
                    {
                        operations.Push(token);
                    }

                    //Если приоритет token <= приоритета символа на вершине стека,
                    //то извлекаем символы из стека в очередь, пока выполняется условие и помещаем token в стек
                    else if (token.ToString() != ")")
                    {
                        while (operations.Count() != 0 && GetPriority(token) <= GetPriority(operations.Peek()))
                        {
                            operands.Enqueue(operations.Pop());
                        }
                        operations.Push(token);
                    }

                    //Если ')', то извлекаем символы из стека в очередь,
                    //пока не встретим в стеке '(', которую удаляем, как и ')'
                    else
                    {
                        while (operations.Count() != 0 && operations.Peek().ToString() != "(")
                        {
                            operands.Enqueue(operations.Pop());
                        }
                        if (operations.Count() != 0)
                        {
                            operations.Pop();
                        }
                    }
                }
            }

            //извлекаем из стека в очередь, если в стеке еще есть операции
            while (operations.Count() != 0)
            {
                operands.Enqueue(operations.Pop());
            }

            return operands;
        }

        private static int GetPriority(char token)
        {
            string symbol = token.ToString();

            if (symbol == "^")
                return 4;
            else if (symbol == "*" || symbol == "/")
                return 3;
            else if (symbol == "+" || symbol == "-")
                return 2;
            else
                return 1;
        }

        static bool CheckNumber(string text) //Проверка на число/переменную
        {
            if (double.TryParse(text.ToString(), out _) || text == "x")
                return true;
            else
                return false;
        }
    }
}
