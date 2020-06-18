using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanism_Visualisation
{
    public enum ParamsType { Geometry, Kinematic, Extra}
    class Presenter_params
    {
        public event EventHandler<EventArgs> ReturnParams;

        public ParamsType type { get; private set; }
        IView_params _view;
        public Presenter_params(IView_params iwg, ParamsType type)
        {
            this.type = type;
            _view = iwg;
            _view.ReturnParams += new EventHandler<EventArgs>(Params);
        }
        public void ShowForm()
        {
            _view.ShowForm();
        }
        private void Params(object sender, EventArgs e)
        {
            ReturnParams(this, e);
        }
    }
}
