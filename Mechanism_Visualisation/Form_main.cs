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
    public struct PicIdGraph
    {
        public PictureBox picturebox;
        public int ID;
        public Graphics g;
    }
    public partial class Form_main : Form, IView_Main
    {
        public event EventHandler<EventArgs> Draw;
        public event EventHandler<EventArgs> Clear;
        public event EventHandler<EventArgs> Start;
        public event EventHandler<EventArgs> Stop;
        public event EventHandler<EventArgs> ChangeAB;
        public event EventHandler<EventArgs> GeometryParams;
        public event EventHandler<EventArgs> KinematicParams;
        public event EventHandler<EventArgs> ExtraParams;

        public event EventHandler<EventArgs> State2D;
        public event EventHandler<EventArgs> State3D;

        public event EventHandler<EventArgs> RotateH;
        public event EventHandler<EventArgs> RotateW;
        public event EventHandler<EventArgs> Zoom;
        public event EventHandler<EventArgs> ChangeXray;

        List<PicIdGraph> containers;


        public Form_main()
        {
             
            InitializeComponent();
            containers = new List<PicIdGraph>();
            PicIdGraph pig = new PicIdGraph { picturebox = pictureBox1, ID = 0, g = pictureBox1.CreateGraphics() };
            containers.Add(pig);
            pictureBox1.MouseWheel += new MouseEventHandler(MouseWheele);
            
           // this.MouseWheel += new MouseEventHandler(MouseWheel);
            // hScrollBar1.Value = 180;
        }
        public void ShowForm()
        {
            Application.Run(this);
        }
        private void escapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pointAToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pointAToolStripMenuItem.Checked = !pointAToolStripMenuItem.Checked;
            Dots d = Check();
            EventAB ev = new EventAB(); ev.choise = d;
            ChangeAB(containers[0].ID, ev);
            
        }

        private void pointBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pointBToolStripMenuItem.Checked = !pointBToolStripMenuItem.Checked;
            
            Dots d = Check();
            EventAB ev = new EventAB(); ev.choise = d;
            ChangeAB(containers[0].ID, ev);
          
        }
        private Dots Check()
        {
            if (pointAToolStripMenuItem.Checked && pointBToolStripMenuItem.Checked) return Dots.Both;
            if (pointAToolStripMenuItem.Checked) return Dots.A;
            if (pointBToolStripMenuItem.Checked) return Dots.B;
            
            return Dots.None;
        }
        private void geometryparamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventID ei = new EventID(); ei.ID = containers[0].ID;
            GeometryParams(this, ei);
        }

        private void kinematicparamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventID ei = new EventID(); ei.ID = containers[0].ID;
            KinematicParams(this, ei);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventDraw ed = new EventDraw();
            ed.Graphics = containers[0].g;
            Start(this, ed);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventDraw ed = new EventDraw();
            ed.Graphics = containers[0].g;
            Stop(this, ed);
        }

        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventDraw ed = new EventDraw();
            ed.Graphics = containers[0].g;
            Draw(this, ed);

            Dots d = Check();
            if (d != Dots.None)
            {
                EventAB ev = new EventAB(); ev.choise = d;
                ChangeAB(containers[0].ID, ev);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventDraw ed = new EventDraw();
            ed.Graphics = containers[0].g;
            Clear(this, ed);
        }

        private void exstraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventID ei = new EventID(); ei.ID = containers[0].ID;
            ExtraParams(this, ei);
        }
        public Point ReturnContainerSize(int ID)
        {
            foreach( PicIdGraph pig in containers)
            {
                if (pig.ID == ID) return new Point(pig.picturebox.Width, pig.picturebox.Height);
            }
            return new Point(0, 0);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            pictureBox1.Visible = true;
            vScrollBar1.Enabled = false;
            hScrollBar1.Enabled = false;

            toolStripMenuItem2.Enabled = false;
            dToolStripMenuItem.Enabled = true;
            State2D(this, EventArgs.Empty);
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pictureBox1.Visible = false;
            vScrollBar1.Enabled = true;
            hScrollBar1.Enabled = true;

            dToolStripMenuItem.Enabled = false;
            toolStripMenuItem2.Enabled = true;
            State3D(this, EventArgs.Empty);
        }

        private void Form_main_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            EventKinematicParams ekp = new EventKinematicParams();
            ekp.v_block = vScrollBar1.Value;

            RotateH(this, ekp);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            EventKinematicParams ekp = new EventKinematicParams();
            ekp.v_block = hScrollBar1.Value;

            RotateW(this, ekp);
        }

        void MouseWheele(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                Zoom(true, EventArgs.Empty);
            else
                Zoom(false, EventArgs.Empty);
        }

        private void каркасToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (sender as ToolStripItem);
            if (tsi.Text == "Каркас")
            {
                tsi.Text = "Сплошной";
            }
            else tsi.Text = "Каркас";

            ChangeXray(this, EventArgs.Empty);
        }

        private void Form_main_Scroll(object sender, ScrollEventArgs e)
        {
            
            if ((e.NewValue-e.OldValue) > 0)
                Zoom(true, EventArgs.Empty);
            else
                Zoom(false, EventArgs.Empty);
        }

        bool zooming = false;
        Point start;
        private void Form_main_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                zooming = !zooming;
                if (zooming == true) start = Cursor.Position;
            }
        }

        private void Form_main_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Cursor.Position.X - start.X) < -10&&zooming) { Zoom(true, EventArgs.Empty); start = Cursor.Position; }
            if ((Cursor.Position.X - start.X) > 10&&zooming) { Zoom(false, EventArgs.Empty); start = Cursor.Position; }
        }
    }
}
