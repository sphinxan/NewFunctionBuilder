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

        //1 � ���
        [Test] //�������
        public void RPN_Test() 
        {
            var result = new string(new RPN().Parse("4 * (6 - 2) + 5"));
            Assert.AreEqual("462-*5+", result); //���������, ����� �� �������, � ������ ����������, ���� ���
        }

        //2 ������������������x
        [TestCase("1+2", ExpectedResult = "12+")]
        [TestCase("1+2/1", ExpectedResult = "121/+")]
        public string RPN_Tests(string expression)
        {
            return new string(new RPN().Parse(expression));
        }

        //2 �� ��������
        [TestCaseSource(nameof(TestCases))] //�� ����� ���������� ������ �������� ����� ������
        public void Calculate_Tests(double result, char[] rpn)
        {
            Assert.AreEqual(result, new Calculate().ToCalculate(rpn));
        }
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(3, "121/+".ToCharArray()); //�������� ��� ��������� ������� � ����� ��� ���������� ������� yield return
                yield return new TestCaseData(18, "453-7*+".ToCharArray()); //(������ �� ������� �r, �����������, ������ ���-�, � ����� ������ �������� ����� �� ������� �r)
            }
        }
    }
}