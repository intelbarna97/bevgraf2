using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _GraphicsDLL;

namespace _GraphicsWF
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp;       

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(Image.FromFile("terep2.png"));
            canvas.Width = bmp.Width;
            canvas.Height = bmp.Height;
            canvas.Image = bmp;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    bmp = bmp.Supersampling();
                    canvas.Image = bmp;
                    canvas.Invalidate();
                    break;
                case MouseButtons.Right:                  
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:                    
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {            
        }
    }
}
