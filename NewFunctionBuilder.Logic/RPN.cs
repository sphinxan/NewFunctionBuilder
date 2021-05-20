using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewFunctionBuilder.Logic
{
    public class RPN
    {
        public object[] Parse(string text)
        {
            List<object> expression = ParseExpression(text);
            Queue<object> tokens = ToRPN(expression);

            return tokens.ToArray();
        }

        private List<object> ParseExpression(string text) //выражение в лист объектов
        {
            text = text.Replace(" ", "");
            var expression = new List<object>();

            for (int i = 0; i < text.Length; i++)
            {
                //число
                if (CheckNumber(text[i].ToString()) || ((text[i] == '-' || text[i] == '+')
                    && !CheckNumber(text[i - 1].ToString()) && text[i - 1] != ')'))
                    expression.Add(ReadNumber(text, ref i));
                //скобка
                else if (text[i].ToString() == "(" || text[i].ToString() == ")")
                    expression.Add(text[i].ToString());
                //операция
                else
                    expression.Add(ReadOperation(text, ref i));
            }

            return expression;
        }

        static bool CheckNumber(string text) //Проверка на число/переменную
        {
            if (double.TryParse(text.ToString(), out _) || text == "x")
                return true;

            return false;
        }

        static object ReadNumber(string text, ref int i)
        {
            if (text[i] == 'x')
                return "x";
            else
            {
                var num = new StringBuilder().Append(text[i]);
                while (CheckNumber(text[i + 1].ToString()) || text[i + 1] == ',')
                {
                    i++;
                    num.Append(text[i]);
                }

                return double.Parse(num.ToString());
            }
        }

        static Operations ReadOperation(string text, ref int i)
        {
            var str = new StringBuilder().Append(text[i]);

            while (i < text.Length && !CheckNumber(text[i].ToString()) // не дойдем до конца строки
                && text[i] != '(' && text[i] != ')') //пока не встретим цифру или скобку
            {
                str.Append(text[i++]);
            }

            return ChooseOperation(str.ToString());
        }

        static public Operations ChooseOperation(string op)
        {
            Operations operation = null;

            switch (op.ToString())
            {
                case ("+"):
                    operation = new Plus();
                    break;
                case ("-"):
                    operation = new Minus();
                    break;
                case ("*"):
                    operation = new Multiply();
                    break;
                case ("/"):
                    operation = new Devide();
                    break;
                default:
                    throw new Exception("Неизвестная операция");

            }

            return operation;
        }

        private Queue<object> ToRPN(List<object> expression)
        {
            var operations = new Stack<object>();
            //.Push() Вставляет элемент в верх Stack(помещаем в стек)
            //.Pop() Удаляет элемент из верхней части Stack
            //.Peek() Возвращает элемент, расположенный в верхней части Stack, но не удаляет его
            var operands = new Queue<object>();
            //.Enqueue() Добавляет элемент в конец Queue
            //.Dequeue() удаляет самый старый элемент из начала Queue
            //.Peek() Возвращает самый старый элемент, находящийся в начале Queue, но не удаляет его

            for (int i = 0; i < expression.Count; i++)
            {
                var token = expression[i];

                if (CheckNumber(token.ToString())) //Если число/переменная, помещаем в очередь
                {
                    operands.Enqueue(token);
                }

                else
                {
                    //Если '(', или стек пуст, или символы в нем имеют приоритет < приоритета token, то помещаем token в стек
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
                    //пока не встретим в стеке '(', которую уничтожаем, как и ')'
                    else
                    {
                        while (operations.Peek().ToString() != "(")
                        {
                            operands.Enqueue(operations.Pop());
                        }
                        operations.Pop();
                    }
                }  
            }

            //if вся строка разобрана, а в стеке есть операции, извлекаем их из стека в очередь
            while (operations.Count() != 0)
            {
                operands.Enqueue(operations.Pop());
            }

            return operands;
        }

        private int GetPriority(object token)
        {
            string symbol = token.ToString();

            if (symbol == "*" || symbol == "/")
                return 3;
            else if (symbol == "+" || symbol == " - ")
                return 2;
            else
                return 1;
        }
    }
}
