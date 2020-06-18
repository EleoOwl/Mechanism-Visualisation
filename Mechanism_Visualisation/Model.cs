using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using DirectMatrix = Microsoft.DirectX.Matrix;

namespace Mechanism_Visualisation
{
    public enum Dots { None, A, B, Both }
    public enum State { s2D, s3D }
    public class MechanismInfo
    {
        public readonly Timer Timer;
        public Graphics G;
        public Point Size;
        public int Timems;
        public Dots choise;
        public State state;
        public double rotateY;
        public double rotateZ;
        public int zoom;
        public bool XRay;
        public Mechanism mechanism;
        private Form window;
        public Form Window
        {
            get { return window; }
            set
            {

                if (value != null)
                {
                    window = value;
                    try
                    {

                        //Ошибка возникает здесь
                        PresentParameters d3dpp = new PresentParameters();
                        d3dpp.BackBufferCount = 1;
                        d3dpp.SwapEffect = SwapEffect.Discard;
                        d3dpp.Windowed = true;
                        d3dpp.MultiSample = MultiSampleType.None;
                        d3dpp.EnableAutoDepthStencil = true;
                        d3dpp.AutoDepthStencilFormat = DepthFormat.D16;
                        d3d = new Device(0, DeviceType.Hardware, window, CreateFlags.SoftwareVertexProcessing, d3dpp);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Ошибка инициализации");

                    }
                }
                window = value;
            }
        }

        public Device d3d;
        public readonly int Number;
        public MechanismInfo(Timer t, Graphics g, Form w, Mechanism m)
        {
            Timer = t; G = g;
            mechanism = m;
            
            Window = w;
            rotateY = 180;
            rotateZ = 0;
            state = State.s3D;
            zoom = 200;
            XRay = false;
            if (m != null) Number = m.Number;
            else Number = -1;
            Timems = 0;


        }
    }

    class Model
    {

        public event EventHandler<EventArgs> CheckSize;

        private List<MechanismInfo> Mechanisms;
        private List<List<Point>> trajectories;
        List<List<CustomVertex.PositionColored>> trajectories_vertex;

        private MechanismInfo FindMechanismInfo(Mechanism m)
        {
            foreach (MechanismInfo mi in Mechanisms)
            {
                if (mi.mechanism == m) return mi;
            }
            return new MechanismInfo(null, null, null, null);
        }
        private MechanismInfo FindMechanismInfo(Timer t)
        {
            foreach (MechanismInfo mi in Mechanisms)
            {
                if (mi.Timer == t) return mi;
            }
            return new MechanismInfo(null, null, null, null);
        }
        private MechanismInfo FindMechanismInfo(int ID)
        {
            foreach (MechanismInfo mi in Mechanisms)
            {
                if (mi.mechanism.ID == ID) return mi;
            }
            return new MechanismInfo(null, null, null, null);
        }

        private int lastmodelID;

        public Model()
        {
            Mechanisms = new List<MechanismInfo>();
            trajectories = new List<List<Point>>();
            trajectories_vertex = new List<List<CustomVertex.PositionColored>>();
            lastmodelID = 0;
        }
        public void DrawMechanism(int ID, Graphics g)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.G = g;
                mi.Timems = 0;
                mi.Timer.Stop();
                switch (mi.Number)
                {
                    case 8:
                        Draw_8(mi);
                        break;
                }

            }
            else throw new Exception("Error: element not found");
        }
        public void StartMechanism(int ID, Graphics g)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.G = g;
                mi.Timer.Start();
            }
            else throw new Exception("Error: element not found");
        }

        public void DrawMechanism(int ID, Form window)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.Window = window;
                mi.Timems = 0;
                mi.Timer.Stop();
                switch (mi.Number)
                {
                    case 8:
                        Draw_8_3D(mi);
                        break;
                }

            }
            else throw new Exception("Error: element not found");
        }

        public void DrawMechanism(int ID)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                
                if (mi.state == State.s2D) Draw_8(mi);
                else Draw_8_3D(mi);

            }
            else throw new Exception("Error: element not found");
        }

        public void StartMechanism(int ID, Form window)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.Window = window;
                mi.Timer.Start();
            }
            else throw new Exception("Error: element not found");
        }


        private void Draw_8(MechanismInfo mi)
        {
            if (mi.Number != -1 && mi.G!=null)
            {
                Mechanism_8 mechanism = (Mechanism_8)mi.mechanism;

                mechanism.ReturnState(mi.Timems * 0.05);

                Bitmap bp = new Bitmap(mi.Size.X, mi.Size.Y, mi.G);
                Graphics g = Graphics.FromImage(bp);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                HatchBrush Br = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Black, Color.White);
                Pen p = new Pen(Color.Black);
                SolidBrush SBr = new SolidBrush(Color.White);



                Rectangle Rect = new Rectangle();

                //for static ball
                float y0 = (float)(mechanism.Center.Y - mechanism.R1);
                float x0 = (float)(mechanism.Center.X - mechanism.R1);

                //for rope moving block
                float x = (float)(mechanism.Center.X + mechanism.R1);
                float y = (float)(mechanism.Center.Y + ((mechanism.l - mechanism.R1) / 2 - Math.PI * mechanism.r2 / 2));
                //for ropes
                float xx = (float)(x + 2 * mechanism.r2);
                //for block
                int lv = 100;
                double yb = y0 + mechanism.R1 + lv - mi.Timems * mechanism.v_block * 0.05;

                if ((yb - (y0 + mechanism.R1) < 0.001 || ((y + mechanism.State.deltaO2) - mechanism.Center.Y) < 0.01) && mi.Timems != 0) { mi.Timer.Stop(); return; }

                //clear
                Rect.Location = new Point(0, 0);
                Rect.Size = new Size(bp.Size.Width, bp.Size.Height);
                g.FillRectangle(SBr, Rect);
                //block big static
                Rect.Location = new Point((int)x0, (int)y0);
                Rect.Size = new Size((int)(2 * mechanism.R1), (int)(2 * mechanism.R1));
                g.FillEllipse(SBr, Rect);
                g.DrawEllipse(p, Rect);
                //block small static
                Rect.Location = new Point((int)(mechanism.Center.X - mechanism.r1), (int)(mechanism.Center.Y - mechanism.r1));
                Rect.Size = new Size((int)(2 * mechanism.r1), (int)(2 * mechanism.r1));
                g.FillEllipse(SBr, Rect);
                g.DrawEllipse(p, Rect);
                //bearing draw
                Point side = new Point(mechanism.Center.X - 10, mechanism.Center.Y + 20);
                g.DrawLine(p, side, mechanism.Center);
                side.X = mechanism.Center.X + 10;
                side.Y = mechanism.Center.Y + 20;
                g.DrawLine(p, mechanism.Center, side);

                Rect.Location = new Point(mechanism.Center.X - 15, mechanism.Center.Y + 20);
                Rect.Size = new Size(30, 7);
                g.FillRectangle(Br, Rect);

                g.DrawLine(p, mechanism.Center.X + 15, mechanism.Center.Y + 20, mechanism.Center.X - 15, mechanism.Center.Y + 20);
                //bearing ball
                Rect.Location = new Point(mechanism.Center.X - 5, mechanism.Center.Y - 5);
                Rect.Size = new Size(10, 10);
                g.FillEllipse(SBr, Rect);
                g.DrawEllipse(p, Rect);

                //dot lines

                p.DashStyle = DashStyle.DashDot;
                g.DrawLine(p, (int)(mechanism.Center.X + mechanism.R1 * Math.Sin(mechanism.State.rot1)),
                              (int)(mechanism.Center.Y - mechanism.R1 * Math.Cos(mechanism.State.rot1)),
                              (int)(mechanism.Center.X - mechanism.R1 * Math.Sin(mechanism.State.rot1)),
                              (int)(mechanism.Center.Y + mechanism.R1 * Math.Cos(mechanism.State.rot1)));

                g.DrawLine(p, (int)(mechanism.Center.X - mechanism.R1 * Math.Cos(mechanism.State.rot1)),
                              (int)(mechanism.Center.Y - mechanism.R1 * Math.Sin(mechanism.State.rot1)),
                              (int)(mechanism.Center.X + mechanism.R1 * Math.Cos(mechanism.State.rot1)),
                              (int)(mechanism.Center.Y + mechanism.R1 * Math.Sin(mechanism.State.rot1)));
                p.DashStyle = DashStyle.Solid;
                //rope left
                g.DrawLine(p, x, mechanism.Center.Y, (float)(mechanism.Center.X + mechanism.R1), (int)(y + mechanism.State.deltaO2));
                //rope right
                g.DrawLine(p, xx, (int)(y + mechanism.State.deltaO2), (float)(x + 2 * mechanism.r2), y0);
                //mount
                g.DrawLine(p, xx - 30, y0, xx + 30, y0);
                Rect.Location = new Point((int)(xx - 30), (int)(y0 - 10));
                Rect.Size = new Size(60, 10);
                g.FillRectangle(Br, Rect);
                //blockrope
                g.DrawLine(p, (float)(x0 + (mechanism.R1 - mechanism.r1)), (float)(y0 + mechanism.R1),
                                        (float)(x0 + (mechanism.R1 - mechanism.r1)), (float)(yb));


                Rect.Location = new Point((int)(x0 + (mechanism.R1 - mechanism.r1) - 10), (int)yb);
                Rect.Size = new Size(20, 30);
                g.FillRectangle(Br, Rect);
                g.DrawRectangle(p, Rect);

                //block moving
                Rect.Location = new Point((int)x, (int)(y - mechanism.r2 + mechanism.State.deltaO2));
                Rect.Size = new Size((int)(2 * mechanism.r2), (int)(2 * mechanism.r2));
                g.FillEllipse(SBr, Rect);
                g.DrawEllipse(p, Rect);

                //dots line
                double xo2 = Rect.Location.X + mechanism.r2, yo2 = Rect.Location.Y + mechanism.r2;
                p.DashStyle = DashStyle.DashDot;
                g.DrawLine(p, (int)(xo2 - mechanism.r2 * Math.Sin(mechanism.State.rot2)),
                              (int)(yo2 - mechanism.r2 * Math.Cos(mechanism.State.rot2)),
                              (int)(xo2 + mechanism.r2 * Math.Sin(mechanism.State.rot2)),
                              (int)(yo2 + mechanism.r2 * Math.Cos(mechanism.State.rot2)));

                g.DrawLine(p, (int)(xo2 - mechanism.r2 * Math.Cos(mechanism.State.rot2)),
                              (int)(yo2 + mechanism.r2 * Math.Sin(mechanism.State.rot2)),
                              (int)(xo2 + mechanism.r2 * Math.Cos(mechanism.State.rot2)),
                              (int)(yo2 - mechanism.r2 * Math.Sin(mechanism.State.rot2)));
                p.DashStyle = DashStyle.Solid;
                //trajectories
                if (mi.choise != Dots.None) DrawTrajectory(mi.mechanism.ID, mi.Timems == 0, g);
                mi.G.DrawImage(bp, 0, 0);

                g.Dispose();

                mi.Timems += mi.Timer.Interval;
            }
        }

        public void ClearMechanism(int ID)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                if (mi.state == State.s2D)
                {
                    Bitmap bp = new Bitmap(mi.Size.X, mi.Size.Y, mi.G);
                    Graphics g = Graphics.FromImage(bp);
                    Rectangle Rect = new Rectangle();
                    SolidBrush SBr = new SolidBrush(Color.White);
                    Rect.Location = new Point(0, 0);
                    Rect.Size = new Size(bp.Size.Width, bp.Size.Height);
                    g.FillRectangle(SBr, Rect);
                    mi.G.DrawImage(bp, 0, 0);
                    g.Dispose();
                }
                else
                {
                    mi.d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Wheat, 1.0f, 0);
                    mi.d3d.Present();
                }
            }
        }

        public void StopMechanism(int ID)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null) mi.Timer.Stop();
            else throw new Exception("Error: element not found");
        }

        private void DrawTrajectory(int ID, bool f, Graphics g)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.Number == 8)
            {
                Dots choise = mi.choise;
                Mechanism_8 mechanism = (Mechanism_8)mi.mechanism;

                //Bitmap bp = new Bitmap(mi.Size.X, mi.Size.Y, mi.G);
                //Graphics g = Graphics.FromImage(bp);

                Pen p;
                SolidBrush sBr;
                Point O2 = new Point((int)(mechanism.Center.X + mechanism.R1 + mechanism.r2), (int)(mechanism.Center.Y + ((mechanism.l - mechanism.R1) / 2 - Math.PI * mechanism.r2 / 2) + mechanism.State.deltaO2));
                Point A = new Point((int)(O2.X + mechanism.r2 * 3 * Math.Cos(mechanism.State.rot2) / 4),
                                    (int)(O2.Y - mechanism.r2 * 3 * Math.Sin(mechanism.State.rot2) / 4));

                Point B = new Point((int)(O2.X + mechanism.r2 * Math.Cos(mechanism.State.rot2 + 3 * Math.PI / 4)),
                                    (int)(O2.Y - mechanism.r2 * Math.Sin(mechanism.State.rot2 + 3 * Math.PI / 4)));

                if (trajectories.Count < 2)
                {
                    trajectories.Add(new List<Point>());
                    trajectories.Add(new List<Point>());
                }
                if (f)
                {
                    trajectories[0].Clear();
                    trajectories[1].Clear();
                }
                trajectories[0].Add(A);
                trajectories[1].Add(B);
                trajectories[0].Add(A);
                trajectories[1].Add(B);
                if ((choise == Dots.A) || (choise == Dots.Both))
                {
                    p = new Pen(Color.Red);
                    sBr = new SolidBrush(Color.DarkRed);
                    Point[] arr = trajectories[0].ToArray();
                    g.DrawCurve(p, arr);
                    Point po = trajectories[0].LastOrDefault();
                    g.FillEllipse(sBr, (float)(po.X - 4), (float)(po.Y - 4), 8f, 8f);
                    sBr.Dispose();
                    p.Dispose();
                }
                if ((choise == Dots.B) || (choise == Dots.Both))
                {
                    p = new Pen(Color.MediumPurple);
                    sBr = new SolidBrush(Color.Purple);
                    Point[] arr = trajectories[1].ToArray();
                    g.DrawCurve(p, arr);
                    Point po = trajectories[1].LastOrDefault();
                    g.FillEllipse(sBr, (float)(po.X - 4), (float)(po.Y - 4), 8f, 8f);
                    sBr.Dispose();
                    p.Dispose();
                }

                //mi.G.DrawImage(bp, 0, 0);
                //g.Dispose();
            }
        }

        public void RestartMechanism(int ID, Graphics g)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.Timer.Stop();
                mi.Timems = 0;
                mi.G = g;
                mi.Timer.Start();
            }
            else throw new Exception("Error: element not found");
        }

        public Mechanism CreateMechanism(double r1, double R1, double r2, double l, double v_block, Point Center)
        {
            lastmodelID++;
            Mechanism_8 m = new Mechanism_8(r1, R1, r2, l, v_block, Center, lastmodelID - 1);
            CreateMechanismInfo(m);
            return m;
        }
        private void CreateMechanismInfo(Mechanism m)
        {
            Timer timer = new Timer();
            timer.Interval = 10;
            if (m.Number == 8)
            {
                timer.Tick += new EventHandler(WhichMove);
            }
            Mechanisms.Add(new MechanismInfo(timer, null, null, m));
            CheckSize(m.ID, EventArgs.Empty);
        }

        private void WhichMove(object sender, EventArgs e)
        {
            MechanismInfo mi = FindMechanismInfo((Timer)sender);
            if (mi.state == State.s2D) Draw_8(mi);
            else Draw_8_3D(mi);
        }

        public void ChangeSize(int ID, Point p)
        {
            FindMechanismInfo(ID).Size = p;
        }

        public void ChangeTrajectoryParams(int ID, Dots choise)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.choise = choise;
            }
            // else throw new Exception("Error: element not found");
        }

        public void SetParams(int ID, EventGeomParams e)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null && e.r1 > 0)
            {
                if (mi.Number == 8)
                {
                    Mechanism_8 old = mi.mechanism as Mechanism_8;
                    Mechanism_8 new_m = new Mechanism_8(e.r1, e.R1, e.r2, old.l, old.v_block, old.Center, old.ID);
                    mi.mechanism = new_m;
                }
                RestartMechanism(ID);
                StopMechanism(ID);
                DrawMechanism(ID);
            }

        }
        public void SetParams(int ID, EventKinematicParams e)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null && e.v_block != -10.1)
            {
                if (mi.Number == 8)
                {
                    Mechanism_8 old = mi.mechanism as Mechanism_8;
                    Mechanism_8 new_m = new Mechanism_8(old.r1, old.R1, old.r2, old.l, e.v_block, old.Center, old.ID);
                    mi.mechanism = new_m;
                }
                RestartMechanism(ID);
                StopMechanism(ID);
                DrawMechanism(ID);
            }
        }
        public void SetParams(int ID, EventExtraParams e)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null && e.l_block > 0)
            {
                if (mi.Number == 8)
                {
                    Mechanism_8 old = mi.mechanism as Mechanism_8;
                    Mechanism_8 new_m = new Mechanism_8(old.r1, old.R1, old.r2, e.l_rope, old.v_block, old.Center, old.ID);
                    mi.mechanism = new_m;
                }
                RestartMechanism(ID);
                StopMechanism(ID);
                DrawMechanism(ID);
            }
        }

        public void SetState(int ID, State state)
        {
            FindMechanismInfo(ID).state = state;
            StopMechanism(ID);
        }
        public State GetState(int ID)
        {
            return FindMechanismInfo(ID).state;
        }

        private void DrawTrajectory(int ID, bool f)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.Number == 8)
            {
                Dots choise = mi.choise;
                Mechanism_8 mechanism = (Mechanism_8)mi.mechanism;
                
                Point _O2 = new Point((int)( mechanism.R1 + mechanism.r2), (int)(((mechanism.l - mechanism.R1) / 2 - Math.PI * mechanism.r2 / 2) + mechanism.State.deltaO2));
                Point _A = new Point((int)(_O2.X + mechanism.r2 * 3 * Math.Cos(mechanism.State.rot2) / 4),
                                    (int)(_O2.Y - mechanism.r2 * 3 * Math.Sin(mechanism.State.rot2) / 4));

                Point _B = new Point((int)(_O2.X + mechanism.r2 * Math.Cos(mechanism.State.rot2 + 3 * Math.PI / 4)),
                                    (int)(_O2.Y - mechanism.r2 * Math.Sin(mechanism.State.rot2 + 3 * Math.PI / 4)));

                CustomVertex.PositionColored A = new CustomVertex.PositionColored(new Vector3((float)_A.X, (float)_A.Y,0), Color.Blue.ToArgb());
                A.Color = Color.Blue.ToArgb();

                CustomVertex.PositionColored B = new CustomVertex.PositionColored(new Vector3((float)_B.X, (float)_B.Y, 0), Color.Silver.ToArgb());
                B.Color = Color.Red.ToArgb();

                if (trajectories_vertex.Count < 2)
                {
                    trajectories_vertex.Add(new List<CustomVertex.PositionColored>());
                    trajectories_vertex.Add(new List<CustomVertex.PositionColored>());

                    trajectories_vertex[0].Add(A);
                    trajectories_vertex[1].Add(B);
                }
                if (f)
                {
                    trajectories_vertex[0].Clear();
                    trajectories_vertex[1].Clear();
                }
                trajectories_vertex[0].Add(A);
                trajectories_vertex[1].Add(B);

                //РИСОВАНИЕ КУРВ 3D


                List<CustomVertex.PositionColored> real_trajectoriesA = new List<CustomVertex.PositionColored>();
                List<CustomVertex.PositionColored> real_trajectoriesB = new List<CustomVertex.PositionColored>();

                for (int i = 0; i<trajectories_vertex[0].Count; i++)
                {
                    real_trajectoriesA.Add(new CustomVertex.PositionColored(new Vector3(trajectories_vertex[0][i].X / mi.zoom, trajectories_vertex[0][i].Y / mi.zoom, 0), trajectories_vertex[0][i].Color));
                    real_trajectoriesB.Add(new CustomVertex.PositionColored(new Vector3(trajectories_vertex[1][i].X / mi.zoom, trajectories_vertex[1][i].Y / mi.zoom, 0), trajectories_vertex[1][i].Color));

                }


                mi.d3d.VertexFormat = CustomVertex.PositionColored.Format;
                DirectMatrix displace = DirectMatrix.Translation(-(float)(mechanism.R1 * 2) / mi.zoom, -(float)(mechanism.R1 * 2) /mi.zoom, 0);

                Material kolesoMaterial = new Material();
                kolesoMaterial.Diffuse = Color.Blue;

                mi.d3d.RenderState.Lighting = true;

                if ((choise == Dots.A) || (choise == Dots.Both))
                {
                    mi.d3d.Material = kolesoMaterial;

                    Mesh ball2 = Mesh.Sphere(mi.d3d,(float) mechanism.r2/(4*mi.zoom), 50, 20);
                    mi.d3d.Transform.World = displace *
                       DirectMatrix.Translation((float)(_A.X) / mi.zoom, (float)(_A.Y ) / mi.zoom, 0) *
                       DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                       DirectMatrix.Translation(0, 0, 6f);
                    ball2.DrawSubset(0);
                    ball2.Dispose();
                    
                    
                    mi.d3d.Transform.World = displace* DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) * DirectMatrix.Translation(0, 0, 6f);
                    mi.d3d.DrawUserPrimitives(PrimitiveType.LineStrip, real_trajectoriesA.Count - 1, real_trajectoriesA.ToArray());

                }
                if ((choise == Dots.B) || (choise == Dots.Both))
                {
                    kolesoMaterial.Diffuse = Color.Red;
                    mi.d3d.Material = kolesoMaterial;

                    Mesh ball2 = Mesh.Sphere(mi.d3d, (float)mechanism.r2 / (4 * mi.zoom), 50, 20);
                    mi.d3d.Transform.World = displace *
                       DirectMatrix.Translation((float)(_B.X) / mi.zoom, (float)(_B.Y) / mi.zoom, 0) *
                       DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                       DirectMatrix.Translation(0, 0, 6f);
                    ball2.DrawSubset(0);
                    ball2.Dispose();

                    mi.d3d.Transform.World = displace*DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) * DirectMatrix.Translation(0, 0, 6f);
                    mi.d3d.DrawUserPrimitives(PrimitiveType.LineStrip, real_trajectoriesB.Count-1, real_trajectoriesB.ToArray());
                    
                }
            }
        }

        public void RestartMechanism(int ID, Form window)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.Timer.Stop();
                mi.Timems = 0;
                mi.Window = window;
                mi.Timer.Start();
            }
            else throw new Exception("Error: element not found");
        }
        public void RestartMechanism(int ID)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi.mechanism != null)
            {
                mi.Timer.Stop();
                mi.Timems = 0;
                mi.Timer.Start();
            }
            else throw new Exception("Error: element not found");
        }

        private void Draw_8_3D(MechanismInfo mi)
        {
            Material kolesoMaterial = new Material();
            kolesoMaterial.Diffuse = Color.Purple;
            kolesoMaterial.Specular = Color.Yellow;
            kolesoMaterial.SpecularSharpness = 1;


            if (mi.Number != -1 && mi.Window!=null)
            {
                int koef = mi.zoom;

                Mechanism_8 mechanism = (Mechanism_8)mi.mechanism;
                mechanism.ReturnState(mi.Timems * 0.01);

                //for static ball

                //for rope moving block
                float x = (float)( mechanism.R1+mechanism.r2);
                float y = (float)( ((mechanism.l - mechanism.R1) / 2 - Math.PI * mechanism.r2 / 2));
                //for ropes
                float xx = (float)(x + 2 * mechanism.r2);
                //for block
                int lv = 100;
                double yb = mechanism.R1 + lv - mi.Timems * mechanism.v_block * 0.01;

                if ((yb  <( mechanism.R1/2) || (y + mechanism.State.deltaO2) < 0.01) && mi.Timems != 0&&mi.Timer.Enabled) { mi.Timer.Stop(); return; }



                DirectMatrix displace = DirectMatrix.Translation(-(float)(mechanism.R1 * 2) / koef, -(float)(mechanism.R1*2)/koef,0);



                mi.d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Wheat, 1.0f, 0);
                mi.d3d.BeginScene();
                
                SetupProjections(mi);

                mi.d3d.RenderState.FillMode = (mi.XRay)? Microsoft.DirectX.Direct3D.FillMode.Solid : Microsoft.DirectX.Direct3D.FillMode.WireFrame;
                mi.d3d.RenderState.Lighting = true;

                Mechanism_8 m8 = mi.mechanism as Mechanism_8;

                //Большое неподвижное колесо
                Mesh static_wheel = Mesh.Cylinder(mi.d3d, (float)m8.R1 / koef, (float)m8.R1 / koef, 0.1f, 50, 10);
                
                mi.d3d.Material = kolesoMaterial;
                mi.d3d.Transform.World = DirectMatrix.RotationZ((float)mechanism.State.rot1)* displace *
                    DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) *DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) * 
                   DirectMatrix.Translation(0, 0, 6f) ;
                static_wheel.DrawSubset(0);

                //Вращающееся колесо
                Mesh nonstatic_wheel = Mesh.Cylinder(mi.d3d, (float)m8.r2 / koef, (float)m8.r2 / koef, 0.1f, 50, 10);

                mi.d3d.Transform.World = DirectMatrix.RotationZ(-(float)mechanism.State.rot2) * displace *

                   DirectMatrix.Translation(x / koef, (float)(y + mechanism.State.deltaO2) / koef, 0) *
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180))
                    * DirectMatrix.Translation(0, 0, 6f);
                nonstatic_wheel.DrawSubset(0);

                //Маленькое неподвижное колесо
                Mesh static_wheel1 = Mesh.Cylinder(mi.d3d, (float)m8.r1 / (koef), (float)m8.r1 /koef, 0.1f, 50, 10);
                kolesoMaterial.Diffuse = Color.Tomato;
                mi.d3d.Material = kolesoMaterial;
                mi.d3d.Transform.World =DirectMatrix.RotationZ((float)mechanism.State.rot1) * displace * DirectMatrix.Translation(0, 0, 0.1f)*
                DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) *DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) 
                    *DirectMatrix.Translation(0, 0, 6f);
                
                static_wheel1.DrawSubset(0);

                

                float len = (float)(y + mechanism.State.deltaO2);
                if (len < 0) len = -len;

                kolesoMaterial.Diffuse = Color.DimGray;
                mi.d3d.Material = kolesoMaterial;
                Mesh rope_left = Mesh.Cylinder(mi.d3d,0.01f,0.01f,
                    len/koef, 50, 10
                    );
                mi.d3d.Transform.World =
                    DirectMatrix.RotationX((float)Math.PI/2) * displace *
                   DirectMatrix.Translation((float)(mechanism.R1 )/ koef, len/(2*koef),0)*
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) *DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                   DirectMatrix.Translation(0, 0, 6f);
                rope_left.DrawSubset(0);

                Mesh rope_right = Mesh.Cylinder(mi.d3d, 0.01f, 0.01f,
                    len / koef, 50, 10
                    );
                mi.d3d.Transform.World =
                    DirectMatrix.RotationX((float)Math.PI / 2) * displace *
                   DirectMatrix.Translation((float)(mechanism.R1+2*mechanism.r2) / koef, len / (2 * koef), 0) *
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                   DirectMatrix.Translation(0, 0, 6f);
                rope_right.DrawSubset(0);

                Mesh rope_block = Mesh.Cylinder(mi.d3d, 0.01f, 0.01f,
                    (float)yb/ koef, 50, 10
                    );
                mi.d3d.Transform.World =
                    DirectMatrix.RotationX((float)Math.PI / 2) * displace *
                   DirectMatrix.Translation((float)(-mechanism.r1) / koef, (float)yb / (2 * koef), 0.08f) *
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                   DirectMatrix.Translation(0, 0, 6f);
                rope_block.DrawSubset(0);


                //грузик
                kolesoMaterial.Diffuse = Color.ForestGreen;
                mi.d3d.Material = kolesoMaterial;
                Mesh block = Mesh.Box(mi.d3d, 0.1f, 0.05f, 0.2f);
                
                mi.d3d.Transform.World =
                    DirectMatrix.RotationX((float)Math.PI / 2) * displace *
                   DirectMatrix.Translation((float)(-mechanism.r1) / koef, (float)(yb + 0.05f) /  koef, 0.08f) *
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                   DirectMatrix.Translation(0, 0, 6f);
                block.DrawSubset(0);

                kolesoMaterial.Diffuse = Color.PapayaWhip;
                mi.d3d.Material = kolesoMaterial;
                Mesh ball1 = Mesh.Sphere(mi.d3d, (float)mechanism.r1 / (3 * koef), 50, 20);
                mi.d3d.Transform.World = displace *
                   DirectMatrix.Translation(0,0,-0.05f) *
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                   DirectMatrix.Translation(0, 0, 6f);
                ball1.DrawSubset(0);

                Mesh ball2 = Mesh.Sphere(mi.d3d, (float)mechanism.r1/(3*koef), 50, 20);
                mi.d3d.Transform.World = displace *
                   DirectMatrix.Translation(0,0, 0.15f) *
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                   DirectMatrix.Translation(0, 0, 6f);
                ball2.DrawSubset(0);

                mi.d3d.RenderState.CullMode = Cull.None;
                kolesoMaterial.Diffuse = Color.Purple;
                mi.d3d.Material = kolesoMaterial;

                Mesh platform = Mesh.Polygon(mi.d3d,(float) mechanism.r2/koef, 6);
                mi.d3d.Transform.World = DirectMatrix.RotationX((float)Math.PI / 2) * displace *
                   DirectMatrix.Translation((float)(mechanism.R1+2*mechanism.r2)/koef, 0, 0) *
                   DirectMatrix.RotationX((float)(mi.rotateY * Math.PI / 180)) * DirectMatrix.RotationY((float)(mi.rotateZ * Math.PI / 180)) *
                   DirectMatrix.Translation(0, 0, 6f);
                platform.DrawSubset(0);



                if (mi.choise != Dots.None) DrawTrajectory(mi.mechanism.ID, mi.Timems == 0);

                mi.d3d.EndScene();
                mi.d3d.Present();
                

                if (mi.Timer.Enabled)  mi.Timems += mi.Timer.Interval;
                static_wheel.Dispose();
                static_wheel1.Dispose();
                nonstatic_wheel.Dispose();
                rope_left.Dispose();
                rope_right.Dispose();
                rope_block.Dispose();
                block.Dispose();
                ball1.Dispose();
                ball2.Dispose();
                platform.Dispose();
            }
           
        }

        private void SetupProjections(MechanismInfo mi)
        {
            mi.d3d.Lights[0].Enabled = true;   // Включаем нулевой источник освещения
            mi.d3d.Lights[0].Diffuse = Color.White;         // Цвет источника освещения
            mi.d3d.Lights[0].Position = new Vector3(0, 0, 0); // Задаем координаты
            mi.d3d.Transform.Projection =DirectMatrix.PerspectiveFovLH((float)Math.PI / 4, mi.Window.Width / mi.Window.Height, 1.0f, 50.0f);
            
        }

        public void SetRotateH(int ID, double rot)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi != null)
            {
                mi.rotateY = rot;
            }
        }
        public void SetRotateW(int ID, double rot)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            if (mi != null)
            {
                mi.rotateZ = rot;
            }
        }
        public void SetZoom(int ID, int zoom)
        {
            FindMechanismInfo(ID).zoom = zoom;
        }
        public void Zoom(int ID, bool z)
        {
            FindMechanismInfo(ID).zoom+=z?10:-10;
        }
        public bool IsMechMove(int ID)
        {
            return FindMechanismInfo(ID).Timer.Enabled;
        }
        public void ChangeXRay(int ID)
        {
            MechanismInfo mi = FindMechanismInfo(ID);
            mi.XRay = !mi.XRay;
            
        }
    }
}
