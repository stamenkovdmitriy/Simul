using Simul.Controls;
using Simul.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Simul.ViewModel
{
    public class CompareList
    {


        // сравнеие списков для нахождения совпадений
        public static void CompareisList(List<string> listToCompare, 
            int requiredMatches, Action actionToPerform)
        {
            int matchCount = 0;
            foreach (var bezierLine in UmlBezierLine_Container.umlLine)
            {
                if (listToCompare.Contains(bezierLine.Name))
                {
                    matchCount++;
                    if (matchCount == requiredMatches)
                    {
                        actionToPerform();
                    }
                }
            }
        }

        //  нахождение и показ ошибочных соединений
        public static void CompareisListMistake(List<string> listToCompare,
            int requiredMatches, Action actionToPerform)
        {
            int matchCount = 0;
            int mistakeCount = 0;
            foreach (var bezierLine in UmlBezierLine_Container.umlLine)
            {
                if (listToCompare.Contains(bezierLine.Name))
                {
                    bezierLine.bezierPath.Stroke = new SolidColorBrush(Colors.Green);
                    matchCount++;
                    if (matchCount == requiredMatches)
                    {
                        actionToPerform();
                    }
                }
                else
                {
                    bezierLine.bezierPath.Stroke = new SolidColorBrush(Colors.Red);
                    mistakeCount++;
                }
            }
        }
    }
}
