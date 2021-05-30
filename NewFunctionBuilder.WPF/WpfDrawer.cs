using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewFunctionBuilder.WPF
{
    public class WpfDrawer
    {
        public static Canvas MyCanvas { get; set; }
        public static MainWindow MainWindow { get; set; }
        public static Dictionary<double, double> DrawGraph = new Dictionary<double, double>();

        public WpfDrawer(Canvas myCanvas, Dictionary<double, double> drawFunction)
        {
            MyCanvas = myCanvas;
            DrawGraph = drawFunction;
        }

        public static void SetControls()
        {
            MyCanvas.SizeChanged += Canvas_SizeChanged;
        }

        private static void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawAxes();
            DrawFunction(DrawGraph);
        }

        public static void DrawAxes()
        {
            var Height = MyCanvas.ActualHeight;
            var Width = MyCanvas.ActualWidth;

            MyCanvas.Children.Clear();

            MyCanvas.Children.Add(DrawAxis(0, Width, Height / 2, Height / 2)); //ox
            MyCanvas.Children.Add(DrawArrows(Width - 10, Width, Width - 10, Height / 2 - 5, Height / 2, Height / 2 + 5)); // >

            MyCanvas.Children.Add(DrawAxis(Width / 2, Width / 2, 0, Height)); //oy
            MyCanvas.Children.Add(DrawArrows(Width / 2 - 5, Width / 2, Width / 2 + 5, 10, 0, 10)); // ^
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

        public static void DrawFunction(Dictionary<double, double> DrawGraph)
        {
            var Height = MyCanvas.ActualHeight;
            var Width = MyCanvas.ActualWidth;

            var function = new Polyline
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 2,
            };

            foreach (var i in DrawGraph.Keys)
            {
                var point = new Point
                {
                    X = Width / 2 + i * Step.ValueStep,
                    Y = Height / 2 - DrawGraph[i] * Step.ValueStep,
                };
                function.Points.Add(point);
            }

            MyCanvas.Children.Add(function);
        }
    }
}
