using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using NewFunctionBuilder.Logic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;



namespace NewFunctionBuilder.WPF
{
    public class WpfDrawer
    {

        public static Canvas Canvas { get; set; }
        public static MainWindow MainWindow { get; set; }

        public static void SetControls() //событие
        {
            Canvas.SizeChanged += Canvas_SizeChanged; ;
        }

        private static void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawAxes();
            DrawFunction();
        }

        public static void DrawAxes()
        {
            Canvas.Children.Clear();

            double height = Canvas.ActualHeight; //высота
            double width = Canvas.ActualWidth; //ширина

            Canvas.Children.Add(DrawAxis(0, width, height / 2, height / 2)); //ox
            Canvas.Children.Add(DrawArrows(width - 10, width, width - 10, height / 2 - 5, height / 2, height / 2 + 5)); // >

            Canvas.Children.Add(DrawAxis(width / 2, width / 2, 0, height)); //oy
            Canvas.Children.Add(DrawArrows(width / 2 - 5, width / 2, width / 2 + 5, 10, 0, 10)); // ^
        }

        public static Line DrawAxis(double x1, double x2, double y1, double y2)
        {
            var axis = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            return axis;
        }

        public static Polyline DrawArrows(double X1, double X2, double X3, double Y1, double Y2, double Y3)
        {
            var arrow = new Polyline();
            arrow.Points.Add(new Point(X1, Y1));
            arrow.Points.Add(new Point(X2, Y2));
            arrow.Points.Add(new Point(X3, Y3));
            arrow.Stroke = Brushes.Black;

            return arrow;
        }

        public static void DrawFunction()
        {
            //double minX = -Canvas.ActualWidth / 2 - 1;
            //double maxX = Canvas.ActualWidth / 2 + 1;

            //double minY = -Canvas.ActualHeight / 2 - 1;
            //double maxY = Canvas.ActualHeight / 2 + 1;

            //TextBox expression = (TextBox)MainWindow.FindName("tbExpression").ToString();
        }

    }
}
