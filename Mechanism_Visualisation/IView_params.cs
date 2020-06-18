using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanism_Visualisation
{
    interface IView_params
    {
        event EventHandler<EventArgs> ReturnParams;
        void ShowForm();
    }
}
