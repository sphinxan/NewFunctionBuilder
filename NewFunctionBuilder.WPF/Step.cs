using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Threading.Tasks;

namespace NewFunctionBuilder.WPF
{
    class Step
    {
        private static int step = 2;
        public static int ValueStep
        {
            get//(запись)выполняется только при считывании свойства
            {
                return step;
            }
            set//(чтениe)выполняется при присвоении свойству нового значения
            {
                if (value > 100)
                    step = 100;
                else if (value < 2)
                    step = 2;
                else
                    step = value;
            }
        }
    }
}
