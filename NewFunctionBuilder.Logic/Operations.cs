using System;
using System.Collections.Generic;

namespace NewFunctionBuilder.Logic
{
    public abstract class Operations
    {
        public abstract string Name { get; }
        public abstract int Priority { get; } //приоритет операции
        public abstract int OperandCount { get; } //кол-во операндов

        public abstract double Evaluate(List<object> values);
    }

    /*public class Parenthessis //скобки
    {
        public bool IsOpening { get; }

        public Parenthessis(char parenthesis)
        {
            IsOpening = parenthesis == '(';
        }

        public override string ToString()
        {
            return IsOpening ? "(" : ")";
        }
    }*/

    public class Plus : Operations
    {
        public override string Name => "+";
        public override int Priority => 2;
        public override int OperandCount => 2;

        public override double Evaluate(List<object> values) //оценка
        {
            if (values.Count != 2)
                throw new ArgumentException("Неверное количество аргументов.");
            return (double)values[0] + (double)values[1];
        }
    }

    public class Minus : Operations
    {
        public override string Name => "-";
        public override int Priority => 2;
        public override int OperandCount => 2;

        public override double Evaluate(List<object> values)
        {
            if (values.Count != 2)
                throw new ArgumentException("Неверное количество аргументов.");
            return (double)values[0] - (double)values[1];
        }
    }

    public class Multiply : Operations //умножение
    {
        public override string Name => "*";
        public override int Priority => 3;
        public override int OperandCount => 2;

        public override double Evaluate(List<object> values)
        {
            if (values.Count != 2)
                throw new ArgumentException("Неверное количество аргументов.");
            return (double)values[0] * (double)values[1];
        }
    }

    public class Devide : Operations //деление
    {
        public override string Name => "/";
        public override int Priority => 3;
        public override int OperandCount => 2;

        public override double Evaluate(List<object> values)
        {
            if (values.Count != 2)
                throw new ArgumentException("Неверное количество аргументов.");
            return (double)values[0] / (double)values[1];
        }
    }
}
