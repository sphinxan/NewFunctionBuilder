using NUnit.Framework;
using NewFunctionBuilder.Logic;
using System.Collections.Generic;

namespace NewFunctionBuilder.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        { }

        //1 в лоб
        [Test] //атрибут
        public void RPN_Test() 
        {
            var result = new string(new RPN().Parse("4 * (6 - 2) + 5"));
            Assert.AreEqual("462-*5+", result); //Проверяет, равны ли объекты, и выдает исключение, если нет
        }

        //2 параметризированныx
        [TestCase("1+2", ExpectedResult = "12+")]
        [TestCase("1+2/1", ExpectedResult = "121/+")]
        public string RPN_Tests(string expression)
        {
            return new string(new RPN().Parse(expression));
        }

        //2 на тестсорс
        [TestCaseSource(nameof(TestCases))] //не можем изначально задать тестовый набор данных
        public void Calculate_Tests(double result, char[] rpn)
        {
            Assert.AreEqual(result, new Calculate().ToCalculate(rpn));
        }
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(3, "121/+".ToCharArray()); //начинаем при повторном запуске с места где закончился прошлый yield return
                yield return new TestCaseData(18, "453-7*+".ToCharArray()); //(дойдет до первого уr, остановится, вернет рез-т, а потом начнет работать сразу со второго уr)
            }
        }
    }
}