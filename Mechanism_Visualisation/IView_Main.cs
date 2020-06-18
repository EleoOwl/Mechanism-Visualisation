using System;
using System.Drawing;

namespace Mechanism_Visualisation
{
    interface IView_Main
    {
        event EventHandler<EventArgs> Draw;
        event EventHandler<EventArgs> Clear;
        event EventHandler<EventArgs> Start;
        event EventHandler<EventArgs> Stop;
        event EventHandler<EventArgs> ChangeAB;
        event EventHandler<EventArgs> GeometryParams;
        event EventHandler<EventArgs> KinematicParams;
        event EventHandler<EventArgs> ExtraParams;

        event EventHandler<EventArgs> State2D;
        event EventHandler<EventArgs> State3D;


        event EventHandler<EventArgs> RotateH;
        event EventHandler<EventArgs> RotateW;
        event EventHandler<EventArgs> Zoom;
        event EventHandler<EventArgs> ChangeXray;

        void ShowForm();
        Point ReturnContainerSize(int ID);
    }
}
