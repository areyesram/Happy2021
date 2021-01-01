using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace areyesram
{
    public sealed partial class Form1 : Form
    {
        private static Particle[] _particles;
        private static Color _color;
        private static int _bright;

        private static readonly Random Rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var x = Rnd.Next(ClientRectangle.Width);
            var y = Rnd.Next(ClientRectangle.Height);
            _particles = Enumerable.Range(0, 100).Select(_ =>
            {
                var r = Rnd.NextDouble();
                var a = Rnd.NextDouble() * 2 * Math.PI;
                return new Particle
                {
                    X = x,
                    Y = y,
                    Dx = (float)(Math.Cos(a) * r),
                    Dy = (float)(Math.Sin(a) * r)
                };
            }).ToArray();
            _color = Color.FromArgb(Rnd.Next(256), Rnd.Next(256), Rnd.Next(256));
            _bright = 255;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_particles == null) return;
            var g = e.Graphics;
            var pen = new Pen(Color.FromArgb(_bright, _color));
            foreach (var p in _particles)
            {
                g.DrawEllipse(pen, p.X, p.Y, 2, 2);
                p.X += p.Dx;
                p.Y += p.Dy;
            }
            _bright--;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Close();
        }
    }

    internal class Particle
    {
        internal float X { get; set; }
        internal float Y { get; set; }
        internal float Dx { get; set; }
        internal float Dy { get; set; }
    }
}
