using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Simul.Data;

namespace Simul.Controls
{
    public partial class UmlBezierLine_Control : UserControl
    {
        private Ellipse startPointEllipse;
        private Ellipse endPointEllipse;
        private Ellipse controlPointEllipse;
        private Path bezierPath;

        public static List<UmlBezierLine_Control> bezierLines = new List<UmlBezierLine_Control>();

        public string name;

        public UmlBezierLine_Control(ElectricalContact startContact, ElectricalContact endContact) // Принимаем холст как аргумент
        {
            InitializeComponent();

            
            startPointEllipse = new Ellipse();
            startPointEllipse.Width = 10;
            startPointEllipse.Height = 10;
            startPointEllipse.Fill = new SolidColorBrush(Colors.Blue);
            startPointEllipse.SetBinding(Canvas.LeftProperty, new Binding
            {
                Source = startContact,
                Path = new PropertyPath("(Canvas.Left)")
            });
            startPointEllipse.SetBinding(Canvas.TopProperty, new Binding
            {
                Source = startContact,
                Path = new PropertyPath("(Canvas.Top)")
            });
            endPointEllipse = new Ellipse();
            endPointEllipse.Width = 10;
            endPointEllipse.Height = 10;
            endPointEllipse.Fill = new SolidColorBrush(Colors.Blue);
            endPointEllipse.SetBinding(Canvas.LeftProperty, new Binding
            {
                Source = endContact,
                Path = new PropertyPath("(Canvas.Left)")
            });
            endPointEllipse.SetBinding(Canvas.TopProperty, new Binding
            {
                Source = endContact,
                Path = new PropertyPath("(Canvas.Top)")
            });
            controlPointEllipse = new Ellipse();
            controlPointEllipse.Width = 20;
            controlPointEllipse.Height = 20;
            controlPointEllipse.Fill = new SolidColorBrush(Colors.Green);
            double controlPointX = ((Canvas.GetLeft(startContact) + startContact.ActualWidth / 2) 
                + ( Canvas.GetLeft(endContact) + endContact.ActualWidth / 2)) / 2;
            double controlPointY = (( Canvas.GetLeft(endContact) + endContact.ActualWidth / 2) 
                + ( Canvas.GetTop(endContact) + endContact.ActualHeight / 2)) / 2;
            Canvas.SetLeft(controlPointEllipse, controlPointX); //метод установки координат на вашем холсте
            Canvas.SetTop(controlPointEllipse, controlPointY); // метод установки координат на вашем холсте

            bezierPath = new Path();
            bezierPath.Stroke = Brushes.Blue;
            bezierPath.StrokeThickness = 8;

            

            Canvas canvas = new Canvas();
            canvas.Children.Add(bezierPath);
            canvas.Children.Add(controlPointEllipse);
            canvas.Children.Add(startPointEllipse);
            canvas.Children.Add(endPointEllipse);
            Content = canvas;

            // Присваиваем имя экземпляру на основе имен аргументов
            this.name = $"{startContact.Name}_{endContact.Name}";

            bezierLines.Add(this); // добавление текущего экземпляра в список

            controlPointEllipse.MouseMove += ControlPointEllipse_MouseMove;
            bezierPath.MouseRightButtonDown += BezierPath_MouseRightButtonDown;


        }

        // Метод для определения Canvas, в котором находится элемент
        

        public void UpdateBezierPath()
        {
            Point startPoint = new Point(Canvas.GetLeft(startPointEllipse) + startPointEllipse.Width / 2, Canvas.GetTop(startPointEllipse) + startPointEllipse.Height / 2);
            Point endPoint = new Point(Canvas.GetLeft(endPointEllipse) + endPointEllipse.Width / 2, Canvas.GetTop(endPointEllipse) + endPointEllipse.Height / 2);
            Point controlPoint = new Point(Canvas.GetLeft(controlPointEllipse) + controlPointEllipse.Width / 2, Canvas.GetTop(controlPointEllipse) + controlPointEllipse.Height / 2);


            BezierSegment bezierSegment = new BezierSegment(controlPoint, controlPoint, endPoint, true);
            PathFigure pathFigure = new PathFigure(startPoint, new[] { bezierSegment }, false);
            PathGeometry pathGeometry = new PathGeometry(new[] { pathFigure });

            bezierPath.Data = pathGeometry;
        }  
        

        private void ControlPointEllipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double newX = e.GetPosition(this).X - controlPointEllipse.Width / 2;
                double newY = e.GetPosition(this).Y - controlPointEllipse.Height / 2;

                Canvas.SetLeft(controlPointEllipse, newX);
                Canvas.SetTop(controlPointEllipse, newY);

                UpdateBezierPath();
            }
        }
        private void BezierPath_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // Удаление экземпляра BezierCurveControl
                var parentCanvas = Parent as Canvas;
                if (parentCanvas != null)
                {
                    parentCanvas.Children.Remove(this);

                    // Удаление экземпляра из списка
                    UmlBezierLine_Control.bezierLines.Remove(this);
                    UmlBezierLine_Container.umlLine.Remove(this);

                }
            }
        }

        public string GetName()
        {
            return this.name;
        }
    }
}