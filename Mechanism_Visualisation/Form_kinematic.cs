using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mechanism_Visualisation
{
    public partial class Form_kinematic : Form, IView_params
    {
        public event EventHandler<EventArgs> ReturnParams;

        public Form_kinematic()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            this.ShowDialog();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.Close();
            EventKinematicParams ekp = new EventKinematicParams();
            ekp.v_block = Convert.ToDouble(numericUpDown1.Value);
            ReturnParams(this, ekp);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            EventKinematicParams ekp = new EventKinematicParams();
            ekp.v_block = -10.1;
            ReturnParams(this, ekp);
        }
    }
}
