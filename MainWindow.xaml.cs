using Simul.Views;
using Simul.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using static Simul.Controls.UmlBezierLine_Control;
using Simul.Data;
using Simul.ViewModel;


namespace Simul
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            
            myCanvas.MouseWheel += MyCanvas_MouseWheel;

            PreviewKeyDown += MainWindow_PreviewKeyDown;
            PreviewKeyUp += MainWindow_PreviewKeyUp;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            ResetTimer();
        }

// Таймер
        private readonly DispatcherTimer timer;
        private TimeSpan timeLeft;    
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));

            if (timeLeft <= TimeSpan.Zero)
            {
                timer.Stop();
                MessageBox.Show("Таймер завершен!");
                ResetTimer();
            }

            UpdateTimeLabel();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            //поиск ошибок

            Mistakes();
        }
        //поиск ошибок
        private void Mistakes()
        {
            CompareList.CompareisListMistake(
                ShemList.shemaMistake, 29,
                () => electrodvigatel_Control.StartReverseRotation());

            //поиск ошибок
            GetMistakecs gm = new GetMistakecs();
            gm.UpdateCountLabel(countLabel);
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            ResetTimer();
            UpdateTimeLabel();
            myCanvas.Children.Clear();
            // Удаление экземпляра из списка
            UmlBezierLine_Control.bezierLines.Clear();
            UmlBezierLine_Container.umlLine.Clear();
        }
        private void ResetTimer()
        {
            
            timeLeft = TimeSpan.FromMinutes(10);
            countLabel.Content = null;
        }
        private void UpdateTimeLabel()
        {
            TimeLabel.Content = timeLeft.ToString(@"mm\:ss");
        }


        private bool ctrlPressed = false;
        private bool shiftPressed = false;
 // zoom       // прокрутка 
        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                ctrlPressed = true;
            else if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                shiftPressed = true;
        }
        private void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                ctrlPressed = false;
            else if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                shiftPressed = false;
        }                
        private void MyCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (ctrlPressed && !shiftPressed)
            {
                if (e.Delta > 0)
                {
                    myCanvas.LayoutTransform = new ScaleTransform(myCanvas.LayoutTransform.Value.M11 * 1.1,
                    myCanvas.LayoutTransform.Value.M22 * 1.1);
                }
                else 
                {
                    myCanvas.LayoutTransform = new ScaleTransform(myCanvas.LayoutTransform.Value.M11 / 1.1,
                    myCanvas.LayoutTransform.Value.M22 / 1.1);
                }
            }
            else if (shiftPressed && !ctrlPressed)
            {
                if (scrlCanvas != null)
                {
                    scrlCanvas.ScrollToHorizontalOffset(scrlCanvas.HorizontalOffset - e.Delta);
                    e.Handled = true;
                }
            }  
           
        }          

        
// apparats                
        private void btnPitanieZF_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control pitanie = new Apparat_Control("pitanie", 180, 100, myCanvas, "pitanie_ZF.png");

            ElectricalContact pitanie_3F_L1 = new ElectricalContact("pitanie_3F_L1", 20, 20, 14, 16, pitanie, myCanvas);
            ElectricalContact pitanie_3F_L2 = new ElectricalContact("pitanie_3F_L2", 20, 20, 57, 16, pitanie, myCanvas);
            ElectricalContact pitanie_3F_L3 = new ElectricalContact("pitanie_3F_L3", 20, 20, 102, 16, pitanie, myCanvas);
            ElectricalContact pitanie_3F_N = new ElectricalContact("pitanie_3F_N", 20, 20, 145, 16, pitanie, myCanvas);

        }

        Electrodvigatel_Control electrodvigatel_Control = new Electrodvigatel_Control();
        private void electrodvigatel_ZF_M_Click(object sender, RoutedEventArgs e)
        {
            if (electrodvigatel_Control != null)
            {
                electrodvigatel_Control = new Electrodvigatel_Control();
                electrodvigatel_Control.Name = "electrodvigatel";
                electrodvigatel_Control.Width = 300;
                electrodvigatel_Control.Height = 200;
                Canvas.SetLeft(electrodvigatel_Control, 10);
                Canvas.SetTop(electrodvigatel_Control, 10);
                myCanvas.Children.Add(electrodvigatel_Control);

                ElectricalContact electrodvigatel_V2 = new ElectricalContact("electrodvigatel_V2", 20, 20, 81, 45, electrodvigatel_Control, myCanvas);
                ElectricalContact electrodvigatel_U2 = new ElectricalContact("electrodvigatel_U2", 20, 20, 81, 90, electrodvigatel_Control, myCanvas);
                ElectricalContact electrodvigatel_W2 = new ElectricalContact("electrodvigatel_W2", 20, 20, 81, 128, electrodvigatel_Control, myCanvas);
                ElectricalContact electrodvigatel_W1 = new ElectricalContact("electrodvigatel_W1", 20, 20, 140, 45, electrodvigatel_Control, myCanvas);
                ElectricalContact electrodvigatel_V1 = new ElectricalContact("electrodvigatel_V1", 20, 20, 140, 90, electrodvigatel_Control, myCanvas);
                ElectricalContact electrodvigatel_U1 = new ElectricalContact("electrodvigatel_U1", 20, 20, 140, 128, electrodvigatel_Control, myCanvas);
            }
        }

        private void lampa_HLR_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control lampa_HLR = new Apparat_Control("lampa_HLR", 150, 150, myCanvas, "lampa_HLR.png");

            ElectricalContact lampa_HLR_x1 = new ElectricalContact("lampa_HLR_x1", 20, 20, 19, 10, lampa_HLR, myCanvas);
            ElectricalContact lampa_HLR_x2 = new ElectricalContact("lampa_HLR_x2", 20, 20, 114, 10, lampa_HLR, myCanvas);
            ElectricalContact lampa_HLR_x11 = new ElectricalContact("lampa_HLR_x11", 20, 20, 19, 120, lampa_HLR, myCanvas);
            ElectricalContact lampa_HLR_x22 = new ElectricalContact("lampa_HLR_x22", 20, 20, 114, 120, lampa_HLR, myCanvas);

        }

        private void lampa_HLG_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control lampa_HLG = new Apparat_Control("lampa_HLG", 150, 150, myCanvas, "lampa_HLG.png");

            ElectricalContact lampa_HLG_x1 = new ElectricalContact("lampa_HLG_x1", 20, 20, 19, 10, lampa_HLG, myCanvas);
            ElectricalContact lampa_HLG_x2 = new ElectricalContact("lampa_HLG_x2", 20, 20, 114, 10, lampa_HLG, myCanvas);
            ElectricalContact lampa_HLG_x11 = new ElectricalContact("lampa_HLG_x11", 20, 20, 19, 120, lampa_HLG, myCanvas);
            ElectricalContact lampa_HLG_x22 = new ElectricalContact("lampa_HLG_x22", 20, 20, 114, 120, lampa_HLG, myCanvas);

        }

        private void predohranitel_FU_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control predohranitel_FU = new Apparat_Control("predohranitel_FU", 50, 200, myCanvas, "predohranitel_FU.png");

            ElectricalContact predohranitel_FU_1 = new ElectricalContact("predohranitel_FU_1", 20, 20, 15, 15, predohranitel_FU, myCanvas);
            ElectricalContact predohranitel_FU_2 = new ElectricalContact("predohranitel_FU_2", 20, 20, 15, 170, predohranitel_FU, myCanvas);
        }

        private void btnVycluchatel_S_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control vycluchatel_S = new Apparat_Control("vycluchatel_S", 150, 180, myCanvas, "vycluchatel_S.png");

            ElectricalContact vycluchatel_S_13 = new ElectricalContact("vycluchatel_S_13", 20, 20, 15, 10, vycluchatel_S, myCanvas);
            ElectricalContact vycluchatel_S_21 = new ElectricalContact("vycluchatel_S_21", 20, 20, 115, 10, vycluchatel_S, myCanvas);
            ElectricalContact vycluchatel_S_14 = new ElectricalContact("vycluchatel_S_14", 20, 20, 15, 150, vycluchatel_S, myCanvas);
            ElectricalContact vycluchatel_S_22= new ElectricalContact("vycluchatel_S_22", 20, 20, 115, 150, vycluchatel_S, myCanvas);

        }

        private void btnKontactor_KM_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control kontactor_KM = new Apparat_Control("kontactor_KM", 250, 300, myCanvas, "kontactor_KM.png");

            ElectricalContact kontactor_A1 = new ElectricalContact("kontactor_A1", 20, 20, 66, 15, kontactor_KM, myCanvas);
            ElectricalContact kontactor_A2 = new ElectricalContact("kontactor_A2", 20, 20, 165, 15, kontactor_KM, myCanvas);

            ElectricalContact kontactor_1 = new ElectricalContact("kontactor_1", 20, 20, 16, 65, kontactor_KM, myCanvas);
            ElectricalContact kontactor_3 = new ElectricalContact("kontactor_3", 20, 20, 66, 65, kontactor_KM, myCanvas);
            ElectricalContact kontactor_5 = new ElectricalContact("kontactor_5", 20, 20, 115, 65, kontactor_KM, myCanvas);
            ElectricalContact kontactor_13 = new ElectricalContact("kontactor_13", 20, 20, 163, 65, kontactor_KM, myCanvas);
            ElectricalContact kontactor_21 = new ElectricalContact("kontactor_21", 20, 20, 214, 65, kontactor_KM, myCanvas);

            ElectricalContact kontactor_2 = new ElectricalContact("kontactor_2", 20, 20, 16, 238, kontactor_KM, myCanvas);
            ElectricalContact kontactor_4 = new ElectricalContact("kontactor_4", 20, 20, 66, 238, kontactor_KM, myCanvas);
            ElectricalContact kontactor_6 = new ElectricalContact("kontactor_6", 20, 20, 115, 238, kontactor_KM, myCanvas);
            ElectricalContact kontactor_14 = new ElectricalContact("kontactor_14", 20, 20, 163, 238, kontactor_KM, myCanvas);
            ElectricalContact kontactor_22 = new ElectricalContact("kontactor_22", 20, 20, 214, 238, kontactor_KM, myCanvas);

        }

        private void btnAvtomat_vycl_1F_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control avtomat_vycl_1F = new Apparat_Control("avtomat_vycl_1F", 50, 200, myCanvas, "avtomat_vycl_1F.png");

            ElectricalContact avtomat_vycl_1F_1 = new ElectricalContact("avtomat_vycl_1F_1", 20, 20, 15, 15, avtomat_vycl_1F, myCanvas);
            ElectricalContact avtomat_vycl_1F_2 = new ElectricalContact("avtomat_vycl_1F_2", 20, 20, 15, 170, avtomat_vycl_1F, myCanvas);
        }

        private void btnAvtomat_vycl_3F_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control avtomat_vycl_3F = new Apparat_Control("avtomat_vycl_3F", 150, 200, myCanvas, "avtomat_vycl_3F.png");

            ElectricalContact avtomat_3F_1 = new ElectricalContact("avtomat_3F_1", 20, 20, 16, 11, avtomat_vycl_3F, myCanvas);
            ElectricalContact avtomat_3F_3 = new ElectricalContact("avtomat_3F_3", 20, 20, 65, 11, avtomat_vycl_3F, myCanvas);
            ElectricalContact avtomat_3F_5 = new ElectricalContact("avtomat_3F_5", 20, 20, 114, 11, avtomat_vycl_3F, myCanvas);

            ElectricalContact avtomat_3F_2 = new ElectricalContact("avtomat_3F_2", 20, 20, 16, 170, avtomat_vycl_3F, myCanvas);
            ElectricalContact avtomat_3F_4 = new ElectricalContact("avtomat_3F_4", 20, 20, 65, 170, avtomat_vycl_3F, myCanvas);
            ElectricalContact avtomat_3F_6 = new ElectricalContact("avtomat_3F_6", 20, 20, 114, 170, avtomat_vycl_3F, myCanvas);

        }

        private void btnTteplovoe_rele_3F_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control teplovoe_rele_3F = new Apparat_Control("teplovoe_rele_3F", 250, 250, myCanvas, "teplovoe_rele_3F.png");

            ElectricalContact tepl_rel_1 = new ElectricalContact("tepl_rel_1", 20, 20, 24, 26, teplovoe_rele_3F, myCanvas);
            ElectricalContact tepl_rel_3 = new ElectricalContact("tepl_rel_3", 20, 20, 86, 26, teplovoe_rele_3F, myCanvas);
            ElectricalContact tepl_rel_5 = new ElectricalContact("tepl_rel_5", 20, 20, 147, 26, teplovoe_rele_3F, myCanvas);

            ElectricalContact tepl_rel_98 = new ElectricalContact("tepl_rel_98", 20, 20, 24, 168, teplovoe_rele_3F, myCanvas);
            ElectricalContact tepl_rel_97 = new ElectricalContact("tepl_rel_97", 20, 20, 86, 168, teplovoe_rele_3F, myCanvas);
            ElectricalContact tepl_rel_95 = new ElectricalContact("tepl_rel_95", 20, 20, 147, 168, teplovoe_rele_3F, myCanvas);
            ElectricalContact tepl_rel_96 = new ElectricalContact("tepl_rel_96", 20, 20, 210, 168, teplovoe_rele_3F, myCanvas);

            ElectricalContact tepl_rel_2 = new ElectricalContact("tepl_rel_2", 20, 20, 54, 220, teplovoe_rele_3F, myCanvas);
            ElectricalContact tepl_rel_4 = new ElectricalContact("tepl_rel_4", 20, 20, 115, 220, teplovoe_rele_3F, myCanvas);
            ElectricalContact tepl_rel_6 = new ElectricalContact("tepl_rel_6", 20, 20, 179, 220, teplovoe_rele_3F, myCanvas);

        }

        private void btnRrele_vremeni_KT_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control rele_vremeni_KT = new Apparat_Control("rele_vremeni_KT", 70, 220, myCanvas, "rele_vremeni_KT.png");

            ElectricalContact rele_vremeni_KT_A1 = new ElectricalContact("rele_vremeni_KT_A1", 15, 15, 10, 10, rele_vremeni_KT, myCanvas);
            ElectricalContact rele_vremeni_KT_A2 = new ElectricalContact("rele_vremeni_KT_A2", 15, 15, 45, 10, rele_vremeni_KT, myCanvas);

            ElectricalContact rele_vremeni_KT_35 = new ElectricalContact("rele_vremeni_KT_35", 15, 15, 10, 34, rele_vremeni_KT, myCanvas);
            ElectricalContact rele_vremeni_KT_36 = new ElectricalContact("rele_vremeni_KT_36", 15, 15, 28, 34, rele_vremeni_KT, myCanvas);
            ElectricalContact rele_vremeni_KT_38 = new ElectricalContact("rele_vremeni_KT_38", 15, 15, 47, 34, rele_vremeni_KT, myCanvas);

            ElectricalContact rele_vremeni_KT_25 = new ElectricalContact("rele_vremeni_KT_25", 15, 15, 10, 169, rele_vremeni_KT, myCanvas);
            ElectricalContact rele_vremeni_KT_26 = new ElectricalContact("rele_vremeni_KT_26", 15, 15, 28, 169, rele_vremeni_KT, myCanvas);
            ElectricalContact rele_vremeni_KT_28 = new ElectricalContact("rele_vremeni_KT_28", 15, 15, 47, 169, rele_vremeni_KT, myCanvas);

            ElectricalContact rele_vremeni_KT_15 = new ElectricalContact("rele_vremeni_KT_15", 15, 15, 10, 195, rele_vremeni_KT, myCanvas);
            ElectricalContact rele_vremeni_KT_16 = new ElectricalContact("rele_vremeni_KT_16", 15, 15, 28, 195, rele_vremeni_KT, myCanvas);
            ElectricalContact rele_vremeni_KT_18 = new ElectricalContact("rele_vremeni_KT_8", 15, 15, 47, 195, rele_vremeni_KT, myCanvas);
        }

        private void btnRrele_vremeni_scignal_KT_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control rele_vremeni_scignal_KT = new Apparat_Control("rele_vremeni_scignal_KT", 70, 220, myCanvas, "rele_vremeni_scignal_KT.png");

            ElectricalContact rele_vremeni_scignal_KT_A1 = new ElectricalContact("rele_vremeni_scignal_KT_A1", 15, 15, 10, 10, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_S = new ElectricalContact("rele_vremeni_scignal_KT_S", 15, 15, 28, 10, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_A2 = new ElectricalContact("rele_vremeni_scignal_KT_A2", 15, 15, 45, 10, rele_vremeni_scignal_KT, myCanvas);

            ElectricalContact rele_vremeni_scignal_KT_35 = new ElectricalContact("rele_vremeni_scignal_KT_35", 15, 15, 10, 34, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_36 = new ElectricalContact("rele_vremeni_scignal_KT_36", 15, 15, 28, 34, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_38 = new ElectricalContact("rele_vremeni_scignal_KT_38", 15, 15, 47, 34, rele_vremeni_scignal_KT, myCanvas);

            ElectricalContact rele_vremeni_scignal_KT_25 = new ElectricalContact("rele_vremeni_scignal_KT_25", 15, 15, 10, 169, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_26 = new ElectricalContact("rele_vremeni_scignal_KT_26", 15, 15, 28, 169, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_28 = new ElectricalContact("rele_vremeni_scignal_KT_28", 15, 15, 47, 169, rele_vremeni_scignal_KT, myCanvas);

            ElectricalContact rele_vremeni_scignal_KT_15 = new ElectricalContact("rele_vremeni_scignal_KT_15", 15, 15, 10, 195, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_16 = new ElectricalContact("rele_vremeni_scignal_KT_16", 15, 15, 28, 195, rele_vremeni_scignal_KT, myCanvas);
            ElectricalContact rele_vremeni_scignal_KT_18 = new ElectricalContact("rele_vremeni_scignal_KT_8", 15, 15, 47, 195, rele_vremeni_scignal_KT, myCanvas);
        }

        private void btnKknopochn_post_SB_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control knopochn_post_SB = new Apparat_Control("knopochn_post_SB", 100, 200, myCanvas, "knopochn_post_SB.png");

            Ellipse ellipse1 = new Ellipse();
            ellipse1.Width = 40;
            ellipse1.Height = 40;
            ellipse1.Fill = Brushes.Transparent;
            Canvas.SetLeft(ellipse1, 30);
            Canvas.SetTop(ellipse1, 35);
            knopochn_post_SB.canvasApparat.Children.Add(ellipse1);
            // Добавление обработчика события нажатия на ellipse1
            ellipse1.MouseDown += Ellipse1_MouseDown;

            Ellipse ellipse12 = new Ellipse();
            ellipse12.Width = 40;
            ellipse12.Height = 40;
            ellipse12.Fill = Brushes.Transparent;
            Canvas.SetLeft(ellipse12, 30);
            Canvas.SetTop(ellipse12, 123);
            knopochn_post_SB.canvasApparat.Children.Add(ellipse12);
            // Добавление обработчика события нажатия на ellipse1
            ellipse12.MouseDown += Ellipse2_MouseDown;

            ElectricalContact post_SB2_13 = new ElectricalContact("post_SB2_13", 20, 20, 12, 18, knopochn_post_SB, myCanvas);
            ElectricalContact post_SB2_14 = new ElectricalContact("post_SB2_14", 20, 20, 12, 72, knopochn_post_SB, myCanvas);

                      
            ElectricalContact post_SB2_21 = new ElectricalContact("post_SB2_21", 20, 20, 67, 106, knopochn_post_SB, myCanvas);
            ElectricalContact post_SB2_22 = new ElectricalContact("post_SB2_22", 20, 20, 67, 158, knopochn_post_SB, myCanvas);
        }

        private void btnKknopochn_post_revers_SB_Click(object sender, RoutedEventArgs e)
        {
            Apparat_Control knopochn_post_revers_SB = new Apparat_Control("knopochn_post_revers_SB", 100, 300, myCanvas, "knopochn_post_revers_SB.png");

            Ellipse ellipse11 = new Ellipse();
            ellipse11.Width = 40;
            ellipse11.Height = 40;
            ellipse11.Fill = Brushes.Transparent;
            Canvas.SetLeft(ellipse11, 30);
            Canvas.SetTop(ellipse11, 35);
            knopochn_post_revers_SB.canvasApparat.Children.Add(ellipse11);
            // Добавление обработчика события нажатия на ellipse1
            ellipse11.MouseDown += Ellipse1_MouseDown;

            Ellipse ellipse122 = new Ellipse();
            ellipse122.Width = 40;
            ellipse122.Height = 40;
            ellipse122.Fill = Brushes.Transparent;
            Canvas.SetLeft(ellipse122, 30);
            Canvas.SetTop(ellipse122, 123);
            knopochn_post_revers_SB.canvasApparat.Children.Add(ellipse122);
            // Добавление обработчика события нажатия на ellipse1
            ellipse122.MouseDown += Ellipse2_MouseDown;

            Ellipse ellipse123 = new Ellipse();
            ellipse123.Width = 40;
            ellipse123.Height = 40;
            ellipse123.Fill = Brushes.Transparent;
            Canvas.SetLeft(ellipse123, 30);
            Canvas.SetTop(ellipse123, 35);
            knopochn_post_revers_SB.canvasApparat.Children.Add(ellipse123);
            // Добавление обработчика события нажатия на ellipse1
            ellipse123.MouseDown += Ellipse1_MouseDown;

            ElectricalContact post_SB2_13 = new ElectricalContact("post_SB2_13", 20, 20, 12, 18, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_14 = new ElectricalContact("post_SB2_14", 20, 20, 12, 72, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_23 = new ElectricalContact("post_SB2_23", 20, 20, 67, 18, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_24 = new ElectricalContact("post_SB2_24", 20, 20, 67, 72, knopochn_post_revers_SB, myCanvas);

            ElectricalContact post_SB2_11 = new ElectricalContact("post_SB2_11", 20, 20, 12, 110, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_12 = new ElectricalContact("post_SB2_12", 20, 20, 12, 168, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_21 = new ElectricalContact("post_SB2_21", 20, 20, 67, 110, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_22 = new ElectricalContact("post_SB2_22", 20, 20, 67, 168, knopochn_post_revers_SB, myCanvas);

            ElectricalContact post_SB2_33 = new ElectricalContact("post_SB2_33", 20, 20, 12, 204, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_34 = new ElectricalContact("post_SB2_34", 20, 20, 12, 256, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_43 = new ElectricalContact("post_SB2_43", 20, 20, 67, 204, knopochn_post_revers_SB, myCanvas);
            ElectricalContact post_SB2_44 = new ElectricalContact("post_SB2_44", 20, 20, 67, 259, knopochn_post_revers_SB, myCanvas);
        }

        
        private  void Ellipse1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CompareList.CompareisList(
                ShemList.shemaLine_Pit_Avt3F_PostSB2_Eldv, 8,
                () => electrodvigatel_Control.StartRotation());

            CompareList.CompareisList(
               ShemList.shemaLine_Pit_Avt3F_PostSB2_Eldv_Revers, 8,
               () => electrodvigatel_Control.StartReverseRotation());

            CompareList.CompareisList(
                ShemList.shemaLine, 19,
                () => electrodvigatel_Control.StartRotation());

            CompareList.CompareisList(
                ShemList.shemaLineRevers, 19,
                () => electrodvigatel_Control.StartReverseRotation());

        }
          
        private  void Ellipse2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            electrodvigatel_Control.StopRotation();
        }


// окна схемы и лабораторные
        private void shem_Click(object sender, RoutedEventArgs e)
        {
            ShemWindow1 w = new ShemWindow1();
            w.Show();
        }

        private void lab_Click(object sender, RoutedEventArgs e)
        {
            LaboratornyWindow1 l =new LaboratornyWindow1();
            l.Show();
        }

// кнопки для тестирования
        int counter = 0;
        private void start_Click(object sender, RoutedEventArgs e)
        {
            foreach (var bezierLine in bezierLines)
            {
                if (ShemList.shemaLine.Contains(bezierLine.Name))
                {
                    counter++;
                    // Действия, которые нужно выполнить при совпадении
                    if (counter == 19)
                        electrodvigatel_Control.StartRotation();
                }
            }
            CheckBezierLines();
        }

        private void CheckBezierLines()
        {
            if (UmlBezierLine_Control.bezierLines.Count > 0)
            {
                foreach (var bezierLine in UmlBezierLine_Control.bezierLines)
                {
                    MessageBox.Show(bezierLine.Name); // Выводим имя экземпляра
                }
            }
            else
            {
                MessageBox.Show("Список bezierLines пуст");
            }

        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            electrodvigatel_Control.StopRotation();
        }
    }
}
