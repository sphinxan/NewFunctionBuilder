using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using NewFunctionBuilder.Logic;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


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

            WpfDrawer.MainWindow = this;
            WpfDrawer.Canvas = (Canvas)FindName("Сanvas");
            WpfDrawer.SetControls();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            string expression = tbExpression.Text;
            double xMin = double.Parse(tbXMin.Text);
            double xMax = double.Parse(tbXMax.Text);
            double step = double.Parse(tbStep.Text);

            Calculate function = new Calculate(expression, xMin, xMax, step);

            spRPN.Visibility = Visibility.Visible;
            string rpnString = function.RpnStr(expression);
            tbRPN.Text = "  " + new string(rpnString);

            spResult.Visibility = Visibility.Visible;
            tbResult.Text = "  " + function.FunctionValues().First().Value.ToString();
            tbYCoord.Text = function.FunctionValues().First().Value.ToString();

            WpfDrawer.DrawAxes();
            btnTable.Visibility = Visibility.Visible;

            WpfDrawer.DrawFunction();

            Dictionary<double, double> functionValues = function.FunctionValues();
            gValues.ItemsSource = functionValues.Select(x => new Values { X = x.Key, Y = x.Value }).ToList();
        }

        private void BtnTable_Click(object sender, RoutedEventArgs e)
        {
            gValues.Visibility = Visibility.Visible;
        }
    }
}
