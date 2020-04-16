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
            double xoffset = 60;
            double yoffset = 185;
            double factor = 20;
            return new Point((int)(xoffset + factor * x), (int)(yoffset + factor * -y));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = panel2.CreateGraphics();
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(Color.Black);

            FontFamily fontFamily = new FontFamily("Arial");
            Font f = new Font(
               fontFamily,
               16,
               FontStyle.Regular,
               GraphicsUnit.Pixel);

            g.DrawLine(p, p2p(-35, 0), p2p(35, 0));   // x achse
            g.DrawLine(p, p2p(0, -9), p2p(0, 9));    // y achse

            g.DrawLine(p, p2p(35, 0), p2p(34.5, -0.5));  // x Pfeil
            g.DrawLine(p, p2p(35, 0), p2p(34.5, 0.5));

            g.DrawLine(p, p2p(0, 9), p2p(0.5, 8.5));   // y Pfeil
            g.DrawLine(p, p2p(0, 9), p2p(-0.5, 8.5));

            g.DrawLine(p, p2p(-0.5, -0.5), p2p(0.5, 0.5));   // 0 linie


            for (double i = 0; i < 35; i += 1)
            {
                g.DrawLine(p, p2p(i, -0.25), p2p(i, 0.25));
            }

            for (double ii = 0; ii < 9; ii += 1)
            {
                g.DrawLine(p, p2p(0.25,ii), p2p(-0.25, ii));
            }



            double y, x_old = 0, y_old = 0;
            double y2;
            double y3;
            bool is_first = true;
            double periode = 1;
            double periode2 = 2;
            double periode3 = 3;
            double hoehe = 4;
            double multiplie = m_multiplier;
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

            String text = "0";
            g.DrawString(text, f, b, p2p(-1, -0.25));

            string[] ax = new string[10];
            string[] ay = new string[10];
            //   double[] ax2 = new double[10];

            for(int i = 2; i < 9; i += 2)
            {
                double pos2 = (double)i / multiplie;
                ay[i / 2] = pos2.ToString();

                int length2 = ay[i / 2].Length;
                double minus = 0.01;

                if (length2 == 1)
                {
                    minus = 1;
                }
                else if (length2 == 2)
                {
                    minus = 1.4;
                }
                else if (length2 == 3)
                {
                    minus = 1.7;
                }
                else if (length2 == 4)
                {
                    minus = 1.9;
                }
                else if (length2 == 5)
                {
                    minus = 2.2;
                }else if (length2 > 5)
                {
                    ay[i/2] = ay[i/2].Substring(0, 5);
                    minus = 2.6;
                }


                g.DrawString(ay[i / 2], f, b, p2p(- minus,i+0.4));
            }

            for (int i = 5; i < 35; i += 5)
            {
                double pos = (double)i / multiplie;
                ax[i / 5] = pos.ToString();

                int length = ax[i / 5].Length;
                double minus = 0.1;

                if(length == 1)
                {
                    minus = 0.35;
                }else if(length == 2)
                {
                    minus = 0.52;
                }else if(length == 3)
                {
                    minus = 0.75;
                }else if (length == 4)
                {
                minus = 1;
                }
                else if (length == 5)
                {
                    minus = 1.25;
                } else if (length > 5)
                {
                    ax[i / 5] = ax[i / 5].Substring(0, 5);
                    minus = 1.25;
                }
                g.DrawString(ax[i/5], f, b, p2p(i - minus, -0.2));
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            // label2 =
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void trackBar1_Validated(object sender, EventArgs e)
        {
        }

        private void trackBar1_MouseCaptureChanged(object sender, EventArgs e)
        {
            double v = trackBar1.Value;
            m_multiplier = v/10;
            if (m_multiplier == 0)
            {
                m_multiplier = 0.1;
            }else 
          //  panel1.Refresh();
            panel2.Refresh();
        }
        private double m_multiplier = 1;
        
    }
}
