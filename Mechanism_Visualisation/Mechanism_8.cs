using System;
using System.Drawing;

namespace Mechanism_Visualisation
{
    //public struct Point
    //{
    //    public double X;
    //    public double Y;
    //}
    public class State_8 : MechanismState
    {
        public new double Number = 8;

        public double rot1;
        public  double rot2;
        public double deltaO2;
    }

    public  class Mechanism_8: Mechanism
    {

        public double r1 { get; private set; }
        public double R1 { get; private set; }
        public double r2 { get; private set; }
        public double v_block { get; private set; }
        public double l { get; private set; }

        public State_8 State { get; private set; }
        public Mechanism_8 Default { get { return new Mechanism_8(50,100,70,280, 4, new Point(100,100), ID); }}

        public Mechanism_8(double r1, double R1, double r2, double l, double v_block, Point c, int id)
        {
            this.r1 = Math.Abs(r1);
            this.R1 = Math.Abs(R1);
            if ((R1-r1)< 0)
            {
                R1 = 2 * r1;
            } 
            this.r2 = Math.Abs(r2);
            this.v_block = v_block;
            State = new State_8 { rot1 = 0, rot2 = 0, deltaO2 = 0 };
            Center = c;
            Number = 8;
            ID = id;
            if (l < 2*R1+r2*Math.PI) this.l = 4 * (R1 +r2);
                     else this.l = l;
        }
        public State_8 ReturnState(double t)
        {
            double omega1 = v_block / r1,
                    v_rope = omega1 * R1;
            State = new State_8 { rot1 = omega1 * t, rot2 = v_rope * t / r2, deltaO2 = v_rope * t};
            return State;
        }
        public string[] Info()
        {
            string[] param = new string[4];
            param[0] = "Скорость блока: "+ v_block;
            param[1] = "Радиус R1: " + R1;
            param[2] = "Радиус r1: " + r1;
            param[3] = "Радиус r2: " + r2;
            return param;
        }
    }
}
