using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mechanism_Visualisation
{
    public abstract class Mechanism
    {
        public int Number { get; protected set; }
        public int ID { get; protected set; }
        public Point Center { get; protected set; }
    }
    public abstract class MechanismState
    {
        public int Number { get; }
    }
}
