using System;
using System.Drawing;

namespace Mechanism_Visualisation
{
    class Presenter_Main
    {
        public event EventHandler<EventArgs> GeometryParams;
        public event EventHandler<EventArgs> KinematicParams;
        public event EventHandler<EventArgs> ExtraParams;



        private IView_Main _view;
        private Model _model;
        private int ID = -1;

        public Presenter_Main(IView_Main im, Model model)
        {
            _view = im;
            _view.Draw += new EventHandler<EventArgs>(OnDraw);
            _view.Clear += new EventHandler<EventArgs>(OnClear);
            _view.Start += new EventHandler<EventArgs>(OnStart);
            _view.Stop += new EventHandler<EventArgs>(OnStop);
            _view.ChangeAB += new EventHandler<EventArgs>(OnChangeAB);
            _view.GeometryParams += new EventHandler<EventArgs>(OnGeometry);
            _view.KinematicParams += new EventHandler<EventArgs>(OnKinematic);
            _view.ExtraParams += new EventHandler<EventArgs>(OnExtra);
            _view.State2D += new EventHandler<EventArgs>(OnState2D);
            _view.State3D += new EventHandler<EventArgs>(OnState3D);
            _view.RotateH+= new EventHandler<EventArgs>(OnRotateH);
            _view.RotateW += new EventHandler<EventArgs>(OnRotateW);
            _view.Zoom+= new EventHandler<EventArgs>(OnZoom);
            _view.ChangeXray+= new EventHandler<EventArgs>((object o, EventArgs e)=> { _model.ChangeXRay(ID); if (!_model.IsMechMove(ID)) _model.DrawMechanism(ID); });



            _model = model;
            _model.CheckSize += new EventHandler<EventArgs>(OnCheckSize);

            CheckID();
        }

        public void ShowForm()
        {
            _view.ShowForm();
        }

        public void OnDraw(object sender, EventArgs e)
        {
            CheckID();
            if (_model.GetState(ID) == State.s2D)
                _model.DrawMechanism(ID, (e as EventDraw).Graphics);
            else _model.DrawMechanism(ID, (sender as System.Windows.Forms.Form));
        }
        public void OnClear(object sender, EventArgs e)
        {
            CheckID();
            _model.ClearMechanism(ID);
        }
        public void OnStart(object sender, EventArgs e)
        {
            CheckID();
            if (_model.GetState(ID) == State.s2D)
                _model.StartMechanism(ID, (e as EventDraw).Graphics);
            else _model.StartMechanism(ID, (sender as System.Windows.Forms.Form));
        }
        public void OnStop(object sender, EventArgs e)
        {
            CheckID();
            _model.StopMechanism(ID);

        }

        private void CheckID()
        {
            if (ID == -1)
            {
                Mechanism_8 m = (Mechanism_8)_model.CreateMechanism(50, 100, 70, 280, 2, new Point(200, 150));
                ID = m.ID;
            }
        }
        public void OnCheckSize(object sender, EventArgs e)
        {
            _model.ChangeSize((int)sender, _view.ReturnContainerSize((int)sender));
        }
        public void OnChangeAB(object sender, EventArgs e)
        {
            _model.ChangeTrajectoryParams((int)sender, (e as EventAB).choise);
        }
        public void OnGeometry(object sender, EventArgs e)
        {
            GeometryParams?.Invoke(this, e);
        }
        public void OnKinematic(object sender, EventArgs e)
        {
            KinematicParams?.Invoke(this, e);
        }
        public void OnExtra(object sender, EventArgs e)
        {
            ExtraParams?.Invoke(this, e);
        }
        public void OnState2D(object sender, EventArgs e)
        {
            _model.SetState(ID, State.s2D);

            if (!_model.IsMechMove(ID)) _model.DrawMechanism(ID);
        }
        public void OnState3D(object sender, EventArgs e)
        {
            _model.SetState(ID, State.s3D);

            if (!_model.IsMechMove(ID)) _model.DrawMechanism(ID);
        }
        public void OnRotateH(object sender, EventArgs e)
        {
            _model.SetRotateH(ID, (e as EventKinematicParams).v_block);
            if (!_model.IsMechMove(ID)) _model.DrawMechanism(ID);
        }
        public void OnRotateW(object sender, EventArgs e)
        {
            _model.SetRotateW(ID, (e as EventKinematicParams).v_block);
            if (!_model.IsMechMove(ID)) _model.DrawMechanism(ID);
        }
        public void OnZoom(object sender, EventArgs e)
        {
            _model.Zoom(ID, (bool)sender);
            if (!_model.IsMechMove(ID)) _model.DrawMechanism(ID);
        }
    }
}
