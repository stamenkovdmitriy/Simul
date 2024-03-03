using Simul.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace Simul.ViewModel
{
    public class GetMistakecs
    {
        //поиск ошибочных соединений для счета ошибок 
        private int GetRedUmlBezierLinesCount()
        {
            int redUmlBezierLinesCount = 0;
            foreach (var umlBezierLine in UmlBezierLine_Container.umlLine)
            {
                if (umlBezierLine.bezierPath.Stroke is SolidColorBrush solidColorBrush
                    && solidColorBrush.Color == Colors.Red)
                {
                    redUmlBezierLinesCount++;
                }
            }
            return redUmlBezierLinesCount;
        }
        public void UpdateCountLabel(System.Windows.Controls.Label c)
        {
            if (c != null)
            {
                int redUmlBezierLinesCount = GetRedUmlBezierLinesCount();
                c.Content = redUmlBezierLinesCount.ToString();
            }
        }
    }
}
