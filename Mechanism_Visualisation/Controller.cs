using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanism_Visualisation
{
    class Controller
    {
        private Presenter_Main presenter_main;
        private Presenter_params presenter_geom;
        int paramID;
        private Model _model;

        public Controller()
        {
            Form_main fm = new Form_main();
            _model = new Model();
            presenter_main = new Presenter_Main(fm, _model);
            presenter_main.GeometryParams += new EventHandler<EventArgs>(G_params_get);
            presenter_main.KinematicParams+= new EventHandler<EventArgs>(K_params_get);
            presenter_main.ExtraParams += new EventHandler<EventArgs>(E_params_get);
        }
        public void Start()
        {
            presenter_main.ShowForm();
        }
        public void G_params_get(object sender, EventArgs e)
        {
            paramID = (e as EventID).ID;
            Form_geom fg = new Form_geom();
            Presenter_params pg = new Presenter_params(fg, ParamsType.Geometry);
            pg.ReturnParams += new EventHandler<EventArgs>(Params_set);
            pg.ShowForm();
            
        }
        public void K_params_get(object sender, EventArgs e)
        {
            paramID = (e as EventID).ID;
            Form_kinematic fg = new Form_kinematic();
            Presenter_params pg = new Presenter_params(fg, ParamsType.Kinematic);
            pg.ReturnParams += new EventHandler<EventArgs>(Params_set);
            pg.ShowForm();

        }
        public void E_params_get(object sender, EventArgs e)
        {
            paramID = (e as EventID).ID;
            Form_extra fg = new Form_extra();
            Presenter_params pg = new Presenter_params(fg, ParamsType.Extra);
            pg.ReturnParams += new EventHandler<EventArgs>(Params_set);
            pg.ShowForm();

        }

        public void Params_set(object sender, EventArgs e)
        {
            switch ((sender as Presenter_params).type)
            {
                case ParamsType.Geometry: _model.SetParams(paramID, (e as EventGeomParams)); break;
                case ParamsType.Kinematic: _model.SetParams(paramID, (e as EventKinematicParams)); break;
                case ParamsType.Extra: _model.SetParams(paramID, (e as EventExtraParams)); break;
            }
        }
        
    }
}
