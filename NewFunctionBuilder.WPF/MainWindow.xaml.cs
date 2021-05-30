using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

            WpfDrawer.MainWindow = this;
            WpfDrawer.MyCanvas = (Canvas)FindName("Сanvas");
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

            btnTable.Visibility = Visibility.Visible;

            Dictionary<double, double> functionValues = function.FunctionValues();
            gValues.ItemsSource = functionValues.Select(x => new Values { X = x.Key, Y = x.Value }).ToList();

            new WpfDrawer((Canvas)FindName("Сanvas"), functionValues);
        }

        private void BtnTable_Click(object sender, RoutedEventArgs e)
        {
            gValues.Visibility = Visibility.Visible;
        }
    }
}
