using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simul.Controls
{
    public interface IHasElectricalContact
    {
        bool IsConnected { get; }
    }
}
