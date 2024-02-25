
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class ElectricalContact : UserControl
    {
        private bool _isConnected; // Приватное поле для хранения состояния подключения

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value; // Установка значения состояния подключения
            }
        }

        public ElectricalContact(string n, int x, int y, int xx, int yy, Apparat_Control a, Canvas c)
        {
            InitializeComponent();

            this.Name = n;
            this.Width = x;
            this.Height = y;
            BindingOperations.SetBinding(this, Canvas.LeftProperty, new Binding
            {
                Path = new PropertyPath("(Canvas.Left)"),
                Source = a,
                Converter = new AddOffsetConverter(),
                ConverterParameter = xx
            });
            BindingOperations.SetBinding(this, Canvas.TopProperty, new Binding
            {
                Path = new PropertyPath("(Canvas.Top)"),
                Source = a,
                Converter = new AddOffsetConverter(),
                ConverterParameter = yy
            });
            c.Children.Add(this);

            this.MouseDown += ElectricalContact_MouseDown;
            this.MouseRightButtonDown += Delet_MouseRightButtonDown;
        }

        public ElectricalContact(string n, int x, int y, int xx, int yy, Electrodvigatel_Control a, Canvas c)
        {
            InitializeComponent();

            this.Name = n;
            this.Width = x;
            this.Height = y;
            BindingOperations.SetBinding(this, Canvas.LeftProperty, new Binding
            {
                Path = new PropertyPath("(Canvas.Left)"),
                Source = a,
                Converter = new AddOffsetConverter(),
                ConverterParameter = xx
            });
            BindingOperations.SetBinding(this, Canvas.TopProperty, new Binding
            {
                Path = new PropertyPath("(Canvas.Top)"),
                Source = a,
                Converter = new AddOffsetConverter(),
                ConverterParameter = yy
            });
            c.Children.Add(this);

            this.MouseDown += ElectricalContact_MouseDown;
            this.MouseRightButtonDown += Delet_MouseRightButtonDown;
        }

        public string name;
        // Обработчик события нажатия на ElectricalContact
        

        private void ElectricalContact_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var electricalContact = (ElectricalContact)sender;
            var canvas = FindCanvas(electricalContact);

            // Проверяем текущее состояние
            if (electricalContact.IsConnected)
            {
                // Если подключено, меняем на отключено
                electricalContact.IsConnected = false;

                // Устанавливаем красный цвет
                electricalContact.Background = Brushes.Red;
            }
            else
            {
                // Меняем состояние на подключено
                electricalContact.IsConnected = true;

                // Устанавливаем зеленый цвет
                electricalContact.Background = Brushes.Green;

                // Если отключено, проверяем количество подключенных контактов
                int connectedCount = 0;
                var connectedContacts = new List<ElectricalContact>();

                // Перебираем все объекты в Canvas
                foreach (var item in canvas.Children)
                {
                    if (item is ElectricalContact contact && contact.IsConnected)
                    {
                        connectedCount++;
                        connectedContacts.Add(contact);
                    }
                }

                if (connectedCount == 1)
                {
                    // Если только один контакт подключен, не делаем ничего
                    // Можно вывести сообщение об ошибке или предупреждение
                }
                else if (connectedCount >= 2)
                {
                    // Если два и более контакта подключены, рисуем линию между ними
                    // Создайте экземпляр UmlBezierLine_Control и добавьте его на Canvas,
                    // используя координаты из connectedContacts
                    var bezierLine = new UmlBezierLine_Control(connectedContacts[0], connectedContacts[1]);
                    // Присваиваем имя экземпляру на основе имен аргументов
                    bezierLine.Name = $"{connectedContacts[0].Name}_{connectedContacts[1].Name}";
                    
                    canvas.Children.Add(bezierLine);
                    // Обновляем путь Безье
                    bezierLine.UpdateBezierPath();
                    UmlBezierLine_Container.umlLine.Add(bezierLine);
                }

                
            }
        }

        
        // Метод для определения Canvas, в котором находится элемент
        private Canvas FindCanvas(UIElement element)
        {
            var parent = VisualTreeHelper.GetParent(element);

            while (parent != null && !(parent is Canvas))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as Canvas;
        }

        private void Delet_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // Удаление экземпляра BezierCurveControl
                var parentCanvas = Parent as Canvas;
                if (parentCanvas != null)
                {
                    parentCanvas.Children.Remove(this);
                }
            }
        }

    }
}
