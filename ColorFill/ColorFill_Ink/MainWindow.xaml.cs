using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows.Resources;
using System.Media;
namespace ColorFill_Ink
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            testCanvas.Background = Brushes.Lavender;




            //te.   = testCanvas.Strokes;
           // testCanvas.Strokes.
        }

        private void button1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Shape temp = new Shape();
            Path te = new Path();

            //te.Data = new PathGeometry(testCanvas.Strokes);
            //GeometryConverter gc = new GeometryConverter();
            //te.Data = (Geometry)gc.ConvertFromString("M 200,200 L200,100");

            //foreach (Stroke temp in testCanvas.Strokes)
            //{
            //    te.Data = temp.GetGeometry();
            //}
            //te.Data = testCanvas.Strokes;

            Path path = new Path();
            path.Fill = new SolidColorBrush(Colors.Blue);
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, 0);
            PathSegmentCollection segmentCollection = new PathSegmentCollection();
            segmentCollection.Add(new LineSegment() { Point = new Point(0, 0) });
            segmentCollection.Add(new LineSegment() { Point = new Point(70, 75) });
            segmentCollection.Add(new LineSegment() { Point = new Point(80, 75) });
            pathFigure.Segments = segmentCollection;
            pathGeometry.Figures = new PathFigureCollection() { pathFigure };
            path.Data = pathGeometry;
            path.Stroke = new SolidColorBrush(Colors.BlueViolet);
            path.StrokeThickness = 3;


            // 定义圆
            Ellipse ee = new Ellipse();
            ee.Stroke = new SolidColorBrush(Colors.Red);
            ee.Width = 200;
            ee.Height = 200;



            drawCanvas.Children.Add(ee);

        }
    }
}
