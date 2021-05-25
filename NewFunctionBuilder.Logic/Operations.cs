using System;
using System.Collections.Generic;

namespace NewFunctionBuilder.Logic
{
    public abstract class Operations
    {
        public abstract string Name { get; }
        public abstract int Priority { get; } //приоритет операции
        public abstract int OperandCount { get; } //кол-во операндов

        public abstract double Evaluate(double[] arguments);
    }

    public class Plus : Operations
    {
        public override string Name => "+";
        public override int Priority => 2;
        public override int OperandCount => 2;

        public override double Evaluate(double[] arguments) //оценка
        {
            return (double)arguments[0] + (double)arguments[1];
        }
    }

    public class Minus : Operations
    {
        public override string Name => "-";
        public override int Priority => 2;
        public override int OperandCount => 2;

        public override double Evaluate(double[] arguments)
        {
            return (double)arguments[0] - (double)arguments[1];
        }
    }

    public class Multiply : Operations //умножение
    {
        public override string Name => "*";
        public override int Priority => 3;
        public override int OperandCount => 2;

        public override double Evaluate(double[] arguments)
        {
            return (double)arguments[0] * (double)arguments[1];
        }
    }

    public class Devide : Operations //деление
    {
        public override string Name => "/";
        public override int Priority => 3;
        public override int OperandCount => 2;

        public override double Evaluate(double[] arguments)
        {
            return (double)arguments[0] / (double)arguments[1];
        }
    }
}
