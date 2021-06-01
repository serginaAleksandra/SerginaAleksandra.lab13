using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace lab13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        Random rnd = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            double[] Statistics = new double[4];
            double[] Frequency = new double[4];
            double[] p = new double[4];
            double[] Xi = new double[4] { 1, 2, 3, 4 };
            int x = 0;

            double Ex = 0;
            double _Ex = 0;

            double Dx = 0;
            double _Dx = 0;

            double chi_squared = 0;

            int max = Convert.ToInt32(textBox3.Text);
            p[0] = Convert.ToDouble(textBox1.Text);
            p[1] = Convert.ToDouble(textBox2.Text);
            p[2] = p[0] * (1 - p[0]);
            p[3] = 1 - (p[0] + p[1] + p[2]);

            int i = 0;


            for (int j = 0; j < 4; j++)
            {
                _Ex += p[j] * Xi[j];
            }


            for (int j = 0; j < 4; j++)
            {
                _Dx += p[j] * Math.Pow(Xi[j], 2);
            }

            _Dx -= Math.Pow(_Ex, 2);

            while (i < max)
            {
                x = Generator(p);
                switch (x)
                {
                    case 0:
                        Statistics[0]++;
                        break;

                    case 1:
                        Statistics[1]++;
                        break;

                    case 2:
                        Statistics[2]++;
                        break;

                    default:
                        Statistics[3]++;
                        break;
                }
                i++;
            }

            for (int j = 0; j < 4; j++)
            {
                Frequency[j] = Statistics[j] / max;
                Ex += Frequency[j] * Xi[j];
                chart1.Series[0].Points.AddXY(Xi[j], Frequency[j]);
            }


            for (int j = 0; j < 4; j++)
            {
                Dx += Frequency[j] * Math.Pow(Xi[j], 2);
            }
            Dx -= Math.Pow(Ex, 2);


            double _chi = 0;
            for (int j = 0; j < 4; j++)
            {
                _chi += Math.Pow(Statistics[j], 2) / (max * p[j]);
            }

            chi_squared = _chi - max;

            chi_squared = Math.Sqrt(chi_squared);
            double err_abs = Math.Abs(Ex - _Ex);
            double err_var = Math.Abs(Dx - _Dx);

            label6.Text = "Average: " + Math.Round(Ex, 2) + " (error = " + Math.Round(err_abs / Math.Abs(_Ex) * 100) + "%)";
            label5.Text = "Variance: " + Math.Round(Dx, 2) + " (error = " + Math.Round((err_var / Math.Abs(_Dx)) * 100) + "%)";


            label4.Text = "Chi-squared: " + Math.Round(chi_squared, 2) + " < 11,07 is";
            
            label7.Visible = true;
            if (chi_squared < 11.07)
            {
                label7.Text = "true";
                label7.ForeColor = Color.Green;
            }
            else
            {
                label7.Text = "false";
                label7.ForeColor = Color.Red;
            }
        }


        private int Generator(double[] p)
        {

            double a = rnd.NextDouble();

            double S = Math.Truncate(Math.Log(a) / Math.Log(1 - p[0]));

            int x = Convert.ToInt32(S);
            return x;
        }
    }
}
