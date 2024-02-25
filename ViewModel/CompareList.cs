using Simul.Controls;
using Simul.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simul.ViewModel
{
    public class CompareList
    {
        // сравнеие списков для нахождения совпадений
        public static void CompareisList(List<string> listToCompare, int requiredMatches, Action actionToPerform)
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
    }
}
