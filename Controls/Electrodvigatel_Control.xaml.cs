using Simul.Data;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simul.Controls
{
    /// <summary>
    /// Логика взаимодействия для Electrodvigatel_Control.xaml
    /// </summary>
    public partial class Electrodvigatel_Control : UserControl, IHasElectricalContact
    {
        public bool IsConnected { get; private set; }

        private RotateTransform _rotateTransform;
        private DoubleAnimation _reverseAnimation;
        private DoubleAnimation _animation;

        public Electrodvigatel_Control()
        {
            InitializeComponent();

            
             
            this.MouseDown += Control_MouseDown;
            this.MouseMove += Control_MouseMove;
            this.MouseUp += Control_MouseUp;

            // Привязываем обработчик события MouseRightButtonDown к событию MouseRightButtonDown контрола
            this.MouseRightButtonDown += Control_MouseRightButtonDown;

            _rotateTransform = new RotateTransform(0, 0.5, 0.5);
            vint.RenderTransformOrigin = new Point(0.5, 0.5);
            vint.RenderTransform = _rotateTransform;

            _animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(.4)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            _reverseAnimation = new DoubleAnimation
            {
                From = 0,
                To = -360, // Вращение в другую сторону
                Duration = new Duration(TimeSpan.FromSeconds(.4)),
                RepeatBehavior = RepeatBehavior.Forever
            };
        }

        public void StartRotation()
        {
            _rotateTransform.BeginAnimation(RotateTransform.AngleProperty, _animation);
        }
        public void StartReverseRotation()
        {
            _rotateTransform.BeginAnimation(RotateTransform.AngleProperty, _reverseAnimation);
        }

        public void StopRotation()
        {
            _rotateTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            _reverseAnimation.BeginAnimation(RotateTransform.AngleProperty, null);
        }

        private bool isDragging;
        private Point offset;
        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = true;
                offset = e.GetPosition(this);
                this.CaptureMouse();
            }

        }
        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point mousePos = e.GetPosition(Parent as UIElement);
                Canvas.SetLeft(this, mousePos.X - offset.X);
                Canvas.SetTop(this, mousePos.Y - offset.Y);
            }
            if (UmlBezierLine_Container.umlLine != null)
            {
                foreach (var line in UmlBezierLine_Container.umlLine)
                {
                    line.UpdateBezierPath(); // Вызов метода UpdateBezierPath() для каждой линии в списке UmlLine
                }
            }
        }
        private void Control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = false;
                this.ReleaseMouseCapture();
            }
        }

        // Обработчик Удаляем текущий экземпляр контрола
        private void Control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Удаляем текущий экземпляр контрола
            DeleteControl();
        }
        // Метод для удаления текущего экземпляра контрола
        private void DeleteControl()
        {
            // Проверяем, что родительский элемент существует и является типом Canvas
            if (this.Parent is Canvas canvas)
            {
                // Удаляем текущий экземпляр контрола из myCanvas
                canvas.Children.Remove(this);
            }
        }


    }
}
