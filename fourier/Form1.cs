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
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black);
            Pen pRed = new Pen(Color.Red);


            g.DrawLine(p, c2p(new Complex(-2, 0)), c2p(new Complex(2, 0)));
            g.DrawLine(p, c2p(new Complex(0, -2)), c2p(new Complex(0, 2)));

            double freq = 1.0 /3; //  m_periode3;

            Complex c1 = new Complex(0, 0);
            int anzahl = 0;
            Complex summe = new Complex(0, 0);
            for (double t = 0; t < 50; t += 0.005)
            {
                Complex c2 = Complex.Exp(-2 * Math.PI * System.Numerics.Complex.ImaginaryOne * freq * t);
                c2 = c2 * gg(t) / 6;
                summe += c2;
                anzahl++;
                g.DrawLine(p, c2p(c1), c2p(c2));
                c1 = c2;
            }
            summe /= anzahl;
            Point pp = c2p(summe);
            g.DrawEllipse(pRed, pp.X-4, pp.Y-4, 8, 8);
        }
        Point p2p(double x, double y)
        {
            double xoffset = 65;
            double yoffset = 185;
            double factor = 20;
            return new Point((int)(xoffset + factor * x), (int)(yoffset + factor * -y));
        }

        private double m_periode1 = 2;
        private double m_periode2 = 3;
        private double m_periode3 = 4;
        private double m_hoehe = 3;
        private double gg(double x)
        {
            double y1 = Math.Sin(2.0 * Math.PI * x / m_periode1);
            double y2 = Math.Sin(2 * Math.PI * x / m_periode2);
            double y3 = Math.Sin(2 * Math.PI * x / m_periode3);
            return y1 + y2 + y3 + m_hoehe;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = panel2.CreateGraphics();
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(Color.Black);
            Pen pRed = new Pen(Color.Red);

            FontFamily fontFamily = new FontFamily("Arial");
            Font f = new Font(
               fontFamily,
               16,
               FontStyle.Regular,
               GraphicsUnit.Pixel);

            g.DrawLine(p, p2p(-1, 0), p2p(35, 0));   // x achse
            g.DrawLine(p, p2p(0, -1), p2p(0, 9));    // y achse

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
            double hoehe = 4;
            double multiplie = m_multiplier;

            double LCM = lcm(m_periode1, m_periode2, m_periode3);

            for(double i = LCM; i < 350; i += LCM)
            {
                g.DrawLine(pRed, p2p(i * multiplie,0),p2p(i * multiplie,9));
            }

        
            for (double x = 0; x < 35/multiplie; x += 0.01)
            {
                y = gg(x);
                if (is_first)
                {
                    is_first = false;
                }
                else
                {
                    g.DrawLine(p, p2p(multiplie*x_old, multiplie * y_old), p2p(multiplie*x,multiplie* y));
                    x_old = x;
                    y_old = y;
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
            m_multiplier = v;
            if (m_multiplier == 0)
            {
                m_multiplier = 0.01;
            }else if(m_multiplier < 50)
            {
                m_multiplier = m_multiplier/ 50;
            }else if(m_multiplier > 50)
            {
                m_multiplier = (m_multiplier - 45) / 5;
            }else if(m_multiplier == 50)
            {
                m_multiplier = 1;
            }

            string test = m_multiplier.ToString();
            label2.Text = test;

            panel2.Refresh();
        }
        private double m_multiplier = 1;

        private void label2_Paint(object sender, PaintEventArgs e)
        {

        }

        static double gcf(double a, double b)
        {
            while (b != 0)
            {
                double temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static double lcm(double a, double b)
        {
            return (a / gcf(a, b)) * b;
        }
        static double lcm(double a, double b, double c)
        {
            return lcm(lcm(a, b), c);
        }

        Point p2p2(double x, double y)
        {
            double xoffset = 65;
            double yoffset = 185;
            double factor = 70;
            return new Point((int)(xoffset + factor * x), (int)(yoffset + factor * -y));
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = panel3.CreateGraphics();
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(Color.Black);

            g.DrawLine(p, p2p2(-1, 0), p2p2(10.5, 0));   // x achse
            g.DrawLine(p, p2p2(0, -2.5), p2p2(0, 2.5));    // y achse


            g.DrawLine(p, p2p2(0, 9), p2p2(0.5, 8.5));   // y Pfeil
            g.DrawLine(p, p2p2(0, 9), p2p2(-0.5, 8.5));

            g.DrawLine(p, p2p2(-0.15, -0.15), p2p2(0.15, 0.15));   // 0 linie


            for (double i = 0; i < 11; i += 1)
            {
                g.DrawLine(p, p2p2(i, -0.1), p2p2(i, 0.1));
            }

            for (double ii = 0; ii < 3; ii += 1)
            {
                g.DrawLine(p, p2p2(0.1, ii), p2p2(-0.1, ii));
                g.DrawLine(p, p2p2(0.1, -ii), p2p2(-0.1, -ii));
            }

        }
    }
}
