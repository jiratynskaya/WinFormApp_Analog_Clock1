using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp_Analog_Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int width = 300, heigth = 300, bara_secunda = 140, bara_minut = 110, bara_ora = 80;
        private int centerX, centerY;
        private Bitmap bitmap;
        private Graphics graph;
        private int[] Coordinata_MinutSecunda(int value1, int value2)
        {
        var coordinata=new int[2];
        value1*=6; // value1=value1*6;
        if (value1>=0 && value1 <=180)
            {
            coordinata[0]=centerX+(int)(value2*Math.Sin(Math.PI*value1/180));
            coordinata[1]=centerY-(int)(value2*Math.Cos(Math.PI*value1/180));
            }
            else
        {
        coordinata[0]=centerX-(int)(value2*-Math.Sin(Math.PI*value1/180));
            coordinata[1]=centerY-(int)(value2*Math.Cos(Math.PI*value1/180));
        }
        return coordinata;
        }

        private int[] Coordinata_Ora(int value1, int value2, int value3)
        {
            var coordinata = new int[2];
            int x = (int)((value1 * 30) + (value2 + 0.5));
            if (x >= 0 && x <= 180)
            {
                coordinata[0] = centerX + (int)(value3 * Math.Sin(Math.PI * x / 180));
                coordinata[1] = centerY - (int)(value3 * Math.Cos(Math.PI * x / 180));
            }
            else
            {
                coordinata[0] = centerX - (int)(value3 * -Math.Sin(Math.PI * x / 180));
                coordinata[1] = centerY - (int)(value3 * Math.Cos(Math.PI * x / 180));
            }
            return coordinata;

        }


       

        private void timer1_Tick(object sender, EventArgs e)
        {
            graph = Graphics.FromImage(bitmap);
            int secunda = DateTime.Now.Second;
            int minut = DateTime.Now.Minute;
            int ora = DateTime.Now.Hour;
            var coordinata_Manus = new int[2];
            graph.Clear(Color.White);
            graph.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, width, heigth );
            graph.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            graph.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(282, 140));
            graph.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            graph.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));
            
            // bara secunda
            coordinata_Manus = Coordinata_MinutSecunda(secunda, bara_secunda);
            graph.DrawLine(new Pen(Color.Red, 1f), new Point (centerX, centerY), new Point(coordinata_Manus[0], coordinata_Manus[1]));

            // bara minut
            coordinata_Manus = Coordinata_MinutSecunda(minut, bara_minut);
            graph.DrawLine(new Pen(Color.Black, 2f), new Point(centerX, centerY), new Point(coordinata_Manus[0], coordinata_Manus[1]));

            // bara ora
            coordinata_Manus = Coordinata_Ora(ora %12, minut, bara_ora);
            graph.DrawLine(new Pen(Color.Blue, 3f), new Point(centerX, centerY), new Point(coordinata_Manus[0], coordinata_Manus[1]));
            graph.Dispose();
            
            pictureBox1.Image = bitmap;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            bitmap = new Bitmap(width + 1, heigth + 1);
            centerX = width / 2;
            centerY = heigth / 2;
            timer1.Start();
        }
    }
}
