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
    public partial class Form_geom : Form, IView_params
    {
        public event EventHandler<EventArgs> ReturnParams;

        public Form_geom()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.Close();
            EventGeomParams egp = new EventGeomParams();
            egp.R1 = Convert.ToDouble(numericUpDown1.Value);
            egp.r1 = Convert.ToDouble(numericUpDown2.Value);
            egp.r2 = Convert.ToDouble(numericUpDown3.Value);
            ReturnParams?.Invoke(this, egp);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            EventGeomParams egp = new EventGeomParams();
            egp.R1 =  egp.r1 = egp.r2 = -1;
            ReturnParams?.Invoke(this, egp);
        }
        public void ShowForm()
        {
            this.ShowDialog();
        }
        
    }
}
