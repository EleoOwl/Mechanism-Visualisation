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
    public partial class Form_extra : Form, IView_params
    {
        public event EventHandler<EventArgs> ReturnParams;

        public Form_extra()
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
            EventExtraParams eep = new EventExtraParams();
            eep.l_rope = Convert.ToDouble(numericUpDown1.Value);
            eep.l_block = 300;
            ReturnParams?.Invoke(this, eep);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            EventExtraParams eep = new EventExtraParams();
            eep.l_rope = -1;
            eep.l_block = -1;
            ReturnParams?.Invoke(this, eep);
        }
    }
}
