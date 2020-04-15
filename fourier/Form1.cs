using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace fourier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        Point c2p(System.Numerics.Complex c)
        {
            double offset = 200;
            return new Point((int)(offset + offset * c.Real), (int)(offset + offset * -c.Imaginary));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            double rounds = 5;
            double frequency = 5;

            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black);

            g.DrawLine(p, c2p(new Complex(-2, 0)), c2p(new Complex(2, 0)));
            g.DrawLine(p, c2p(new Complex(0, -2)), c2p(new Complex(0, 2)));

            System.Numerics.Complex c1 = new System.Numerics.Complex(0, 0);
            double steps = 1000;
            for (double s = 0; s < rounds*steps; s++)
            {
                Complex c2 = Complex.Exp(-2 * Math.PI * System.Numerics.Complex.ImaginaryOne * (s / steps));
                c2 = c2 * Math.Sin(frequency * s / steps); 
                g.DrawLine(p, c2p(c1), c2p(c2));
                c1 = c2;
            }
        }
        Point p2p(double x, double y)
        {
            double xoffset = 20;
            double yoffset = 185;
            double factor = 20;
            return new Point((int)(xoffset + factor * x), (int)(yoffset + factor * -y));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel2.CreateGraphics();
            Brush b = new SolidBrush(Color.Red);
            Pen p = new Pen(Color.Black);

            g.DrawLine(p, p2p(-35, 0), p2p(35, 0));
            g.DrawLine(p, p2p(0, -9), p2p(0, 9));

            g.DrawLine(p, p2p(35, 0), p2p(34.5, -0.5));
            g.DrawLine(p, p2p(35, 0), p2p(34.5, 0.5));

            g.DrawLine(p, p2p(0, 9), p2p(0.5, 8.5));
            g.DrawLine(p, p2p(0, 9), p2p(-0.5, 8.5));

            g.DrawLine(p, p2p(-0.5, -0.5), p2p(0.5, 0.5));

            for (double i = 0; i < 35; i += 1)
            {
                g.DrawLine(p, p2p(i, -0.25), p2p(i, 0.25));
            }

            for (double ii = 0; ii < 9; ii += 1)
            {
                g.DrawLine(p, p2p(0.25,ii), p2p(-0.25, ii));
            }

            // g.DrawLine(p, c2p2(3, 1)), c2p2(26, 8)));

            double y, x_old = 0, y_old = 0;
            double y2;
            double y3;
            bool is_first = true;
            double periode = 5;
            double periode2 = 4;
            double periode3 = 3;
            double hoehe = 4;
            double multiplie = 0.5;
            for (double x = 0; x < 35/multiplie; x += 0.01)
            {
                y = Math.Sin(2 * Math.PI*x/periode* 2);
                y2 = Math.Sin(2 * Math.PI * x / periode2 *2);
                y3 = Math.Sin(2 * Math.PI * x / periode3 * 2);
                if (is_first)
                {
                    is_first = false;
                }
                else
                {
                    g.DrawLine(p, p2p(multiplie*x_old, multiplie * y_old + hoehe), p2p(multiplie*x,multiplie* (y + y2 + y3) + hoehe));
                    x_old = x;
                    y_old = y + y2 + y3;
                }
            }
            // bla test fjdlafd
        }
    }
}
