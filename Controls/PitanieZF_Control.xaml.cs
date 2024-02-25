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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Simul;
using Simul.Data;

namespace Simul.Controls
{
    
    public partial class PitanieZF_Control : UserControl, IHasElectricalContact
    {

        public bool IsConnected { get; private set; }


        public PitanieZF_Control()
        {
            InitializeComponent();            

            this.MouseDown += PitanieZF_Control_MouseDown;
            this.MouseMove += PitanieZF_Control_MouseMove;
            this.MouseUp += PitanieZF_Control_MouseUp;

            // Привязываем обработчик события MouseRightButtonDown к событию MouseRightButtonDown контрола
            this.MouseRightButtonDown += PitanieZF_Control_MouseRightButtonDown;

                        
        }
        

        // перемещение control
        private bool isDragging;
        private Point offset;
        private void PitanieZF_Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = true;
                offset = e.GetPosition(this);
                this.CaptureMouse();
            }
            
        }
        private void PitanieZF_Control_MouseMove(object sender, MouseEventArgs e)
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
        private void PitanieZF_Control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = false;
                this.ReleaseMouseCapture();
            }            
        }

        // Обработчик Удаляем текущий экземпляр контрола
        private void PitanieZF_Control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
