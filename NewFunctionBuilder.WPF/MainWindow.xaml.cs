using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using NewFunctionBuilder.Logic;

namespace NewFunctionBuilder.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //partial-частичный 
    {
        public MainWindow()
        {
            InitializeComponent(); //вызывает код XAML

            /*eva
            Drawer.MainWindow = this;
            Drawer.GraphCanvas = (Canvas)FindName("Canvas");
            Drawer.SetControls();*/
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            string expression = tbExpression.Text;

            var rpn = new RPN().Parse(expression);

            //Visibility свойство устанавливает параметры видимости элемента(сначала Collapsed - не виден и не участвует в компоновке)
            //Visible - элемент виден и участвует в компоновке
            spRPN.Visibility = Visibility.Visible;
            tbRPN.Text = "  " + new string(new RPN().Parse(expression));

            spResult.Visibility = Visibility.Visible;
            tbResult.Text = "  " + new Calculate().ToCalculate(rpn).ToString();


            //eva
            /*
             if (RPN.IsExpressionCorrectly(((TextBox) FindName("tbFunction")).Text)) 
            {
                Drawer.SetFunction(((TextBox)FindName("tbFunction")).Text);
                Drawer.DrawField();
            }
             */
        }
    }
}
