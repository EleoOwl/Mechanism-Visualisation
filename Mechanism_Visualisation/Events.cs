using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mechanism_Visualisation
{
    public class EventDraw: EventArgs
    {
        public  Graphics Graphics { get; set; }
    }
    public class EventID : EventArgs
    {
        public int ID { get; set; }
    }
    public class EventAB : EventArgs
    {
        public Dots choise { get; set; }
    }
    public class EventGeomParams : EventArgs
    {
        public double r1 { get; set; }
        public double R1 { get; set; }
        public double r2 { get; set; }
    }
    public class EventKinematicParams : EventArgs
    {
        public double v_block { get; set; }
    }
    public class EventExtraParams : EventArgs
    {
        public double l_rope { get; set; }
        public double l_block { get; set; }
    }
}
