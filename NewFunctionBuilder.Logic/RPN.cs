using System;
using System.Collections.Generic;
using System.Text;

namespace NewFunctionBuilder.Logic
{
    public class RPN
    {
        public object[] Parse(string text)
        {
            List<object> expression = ParseExpression(text);
            List<object> tokens = ToRPN(expression);

            return tokens.ToArray();
        }

        private List<object> ParseExpression(string text) //выражение в лист объектов
        {
            text = text.Replace(" ", "");
            List<object> expression = new List<object>();

            for (int i = 0; i < text.Length; i++)
            {
                if (CheckNumber(text[i].ToString()) || ((text[i] == '-' || text[i] == '+') && !CheckNumber(text[i - 1].ToString()) && text[i - 1] != ')')) //Если число
                    expression.Add(ReadNumber(text, ref i));
                else if (text[i].ToString() == "(" || text[i].ToString() == ")")
                    expression.Add(text[i].ToString());
                else
                {
                    expression.Add(ReadOperation(text, ref i));
                }
            }
            return expression;

        }
        static object ReadNumber(string text, ref int i)
        {
            if (text[i] == 'x')
                return "x";
            else
            {
                StringBuilder num = new StringBuilder().Append(text[i]);
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
            StringBuilder str = new StringBuilder().Append(text[i]);
            while (!CheckNumber(text[i + 1].ToString()) && text[i + 1] != '(' && text[i + 1] != ')')
            {
                if ("+-*/".Contains(text[i]))
                    break;
                i++;
                str.Append(text[i]);
            }
            return ChooseOperation(str.ToString());
        }

        static bool CheckNumber(string text) //Проверка на число
        {
            if (double.TryParse(text.ToString(), out _) || text == "x")
                return true;
            return false;
        }

        static public Operations ChooseOperation(object op)
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
            }
            return operation;
        }

        private List<object> ToRPN(List<object> expression)
        {
            List<object> operands = new List<object>();
            List<object> operations = new List<object>();

            for (int i = 0; i < expression.Count; i++)
            {
                object token = expression[i];
                if (CheckNumber(token.ToString()))
                {
                    operands.Add(token);
                }

                /*else if (token is Parenthessis parenthessis && parenthessis.IsOpening)
                {
                    operations.Add(parenthessis);
                }
                else if (token is Parenthessis parenthessis2 && !parenthessis2.IsOpening)
                {

                }*/

                else if (token is Operations op)
                {
                    if (operations.Count != 0 && ((op.Priority) < ((Operations)operations[operations.Count - 1]).Priority))
                    {
                        operands.Add(operations[operations.Count - 1]);
                        operations.RemoveAt(operations.Count - 1);
                    }

                    operations.Add(op);
                }
            }

            for (int i = operations.Count - 1; i >= 0; i--)
            {
                operands.Add(operations[i]);
            }

            return operands;
        }
    }
}
