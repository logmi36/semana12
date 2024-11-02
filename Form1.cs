using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s12p30
{
    public partial class Form1 : Form
    {

        Grafo grafo;

        Point pa;
        Point pb;

        List<Point> la;
        List<Point> lb;

        String na;
        String nb;
        int nc;
        List<string> ma;
        List<string> mb;
        List<string> mc;

        int n;

        SolidBrush brush1;
        SolidBrush brush2;

        FontFamily fontFamily = new FontFamily("Consolas");
        Font font;
        Pen pincel;

        Random rnd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Inicializar();
        }


        private void Inicializar()
        {

            font = new Font(fontFamily, 15, FontStyle.Bold, GraphicsUnit.Pixel);
            brush1 = new SolidBrush(Color.FromArgb(204,204,255));
            brush2 = new SolidBrush(Color.FromArgb(96,96,96));
            pincel = new Pen(Color.FromArgb(139,51,51));
            pincel.Width = 2;

            rnd = new Random();

            n = 0;

            la = new List<Point>();
            lb = new List<Point>();

            ma = new List<string>();
            mb = new List<string>();
            mc = new List<string>();

            na = "";
            nb = "00";
            nc = 0;

            ma.Add(na);
            mb.Add(nb);
            mc.Add("0");

            pa = new Point(0, 0);
            pb = new Point(300, this.Height/2);
            
            la.Add(pa);
            lb.Add(pb);

            AgregarPaneles();
            panel1.Invalidate();
            timer1.Enabled = false;
        }


        private void AgregarPaneles()
        {

            panel1.Controls.Clear();

            for (int i = 0; i < lb.Count; i++)
            {
                Point p1 = new Point(lb[i].X - 20, lb[i].Y - 20);
                Panel pn = new Panel();
                pn.Location = p1;
                pn.Name = "p_" + mb[i];
                //pn.BorderStyle = BorderStyle.FixedSingle;
                pn.Size = new Size(40, 40);
                pn.BackColor = Color.FromArgb(0, 0, 0, 0);
                pn.Click += new System.EventHandler(this.panel_Click);
                panel1.Controls.Add(pn);

                Console.WriteLine("{0}\t{1}\t{2}\t\t\t{3}\t{4}\t\t{5}\t{6}\t{7}", i, ma[i], mb[i], la[i].X, la[i].Y, lb[i].X, lb[i].Y, mc[i]);

            }


            ImplementarGrafo();
        }

        private void ImplementarGrafo() {

            grafo = new Grafo(n+1);

            for (int i = 1; i < mb.Count; i++)
            {

                int a = Convert.ToInt32(ma[i]);
                int b = Convert.ToInt32(mb[i]);

                grafo.AdicionarArista(a,b);

            }


            Console.WriteLine();
            Console.WriteLine();
            grafo.MostrarAdyacencia();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==============================================================");

            grafo.CalcularIndegree();
            grafo.MostrarInDregree();

            Console.WriteLine("==============================================================");

            Console.ForegroundColor = ConsoleColor.Cyan;

            int nodo = 0;

            do
            {
                nodo = grafo.EncuentraI0();
                if (nodo != -1)
                {
                    Console.Write("{0}->", nodo);
                    grafo.DrecremenarInDigree(nodo);
                }
            }
            while (nodo != -1);

            Console.WriteLine();

            Console.WriteLine("==============================================================");

        }


        private void panel_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                UnirNodo(sender);
            }
            else {
                AgregarNodo(sender);
            }

        }


        private void UnirNodo(object sender)
        {

            Panel p1 = (sender as Panel);
            Point p = panel1.PointToClient(Cursor.Position);

            nb = p1.Name.Split('_')[1];

            pb = new Point(p1.Location.X+20, p1.Location.Y+20);
            nc = rnd.Next(0, 20); 


            la.Add(pa);
            lb.Add(pb);

            ma.Add(na);
            mb.Add(nb);
            mc.Add(nc.ToString());

            panel1.Invalidate();
            AgregarPaneles();


            timer1.Stop();

        }

        private void AgregarNodo(object sender)
        {
            Panel p1 = (sender as Panel);
            Point p = panel1.PointToClient(Cursor.Position);

            na = p1.Name.Split('_')[1];

            pa = new Point(p1.Location.X + 20, p1.Location.Y + 20);
            
            timer1.Start();
        }


        

        private void Panel1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {

                timer1.Stop();

                n = n + 1;
                nb = n.ToString("00");
                nc = rnd.Next(0, 20);

                la.Add(pa);
                lb.Add(pb);

                ma.Add(na);
                mb.Add(nb);
                mc.Add(nc.ToString());

                panel1.Invalidate();
                AgregarPaneles();

            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Inicializar();
            }
            if (e.KeyData == Keys.F5)
            {
                panel1.Invalidate();
            }
            if (e.KeyData == Keys.F4)
            {
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (timer1.Enabled)
            {
                e.Graphics.DrawLine(pincel, pa, pb);
            }


            if (lb.Count > 1)
            {
                for (int i = 1; i < lb.Count; i++)
                {

                    double r = -1.0*(lb[i].Y - la[i].Y) / (lb[i].X - la[i].X);
                    double v = Math.Atan(r);

                    if (lb[i].X < la[i].X)
                        v = v + Math.PI;

                    double w = v * 180 / (Math.PI);

                    double d = Math.Sqrt(Math.Pow((lb[i].Y - la[i].Y), 2.0) + Math.Pow((lb[i].X - la[i].X), 2.0)) - 20;
                    double xp = d * Math.Cos(v);
                    double yp = d * Math.Sin(v);
                    Size z1 = new Size(Convert.ToInt32(xp),-1*Convert.ToInt32(yp));
                    xp = 20 * Math.Cos(v);
                    yp = 20 * Math.Sin(v);
                    Size z2 = new Size(Convert.ToInt32(xp), -1 * Convert.ToInt32(yp));
                    Point b2 = Point.Add(la[i], z1);
                    Point b1 = Point.Add(la[i], z2);
                    xp = (d/2) * Math.Cos(v);
                    yp = (d/2) * Math.Sin(v);
                    Size z3 = new Size(Convert.ToInt32(xp), -1 * Convert.ToInt32(yp));
                    Point bc = Point.Add(la[i], z3);

                    //Console.WriteLine("==============================");
                    //Console.WriteLine("A= {0},{1}",la[i].X,la[i].Y);
                    //Console.WriteLine("B= {0},{1}", lb[i].X,lb[i].Y);
                    //Console.WriteLine("r={0}",r);
                    //Console.WriteLine("v={0}", v);
                    //Console.WriteLine("w={0}", w);
                    //Console.WriteLine("d={0}", d);
                    //Console.WriteLine("xp={0}", xp);
                    //Console.WriteLine("yp={0}", yp);


                    pincel.CustomEndCap = new AdjustableArrowCap(5,5);
                    //e.Graphics.DrawLine(pincel, la[i], lb[i]);
                    e.Graphics.DrawLine(pincel, b1, b2);

                    e.Graphics.DrawString(mc[i], font, brush2, bc);

                }
            }

            for (int i = 0; i < lb.Count; i++)
            {
                e.Graphics.FillEllipse(brush1, lb[i].X - 20, lb[i].Y - 20, 40, 40);
                e.Graphics.DrawEllipse(pincel, lb[i].X - 20, lb[i].Y - 20, 40, 40);
                Point tp = new Point(lb[i].X - 10, lb[i].Y - 10);
                e.Graphics.DrawString(mb[i], font, brush2, tp);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
                Point p = panel1.PointToClient(Cursor.Position);
                pb = p;
                panel1.Invalidate();
        }
    }
}
