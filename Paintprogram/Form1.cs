using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Paintprogram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gr = panel1.CreateGraphics();
        }
        bool canPaint = false;
        Graphics gr;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {   
            canPaint = true;
            if (drawSquare)
            {
                SolidBrush s = new SolidBrush(toolStripButton1.ForeColor);
                gr.FillRectangle(s, e.X, e.X, Convert.ToInt32(toolStripTextBox2.Text), Convert.ToInt32(toolStripTextBox2.Text));
                canPaint = false;
                drawSquare = false;
            }
            else if (drawRect)
            {
                SolidBrush s = new SolidBrush(toolStripButton1.ForeColor);
                gr.FillRectangle(s, e.X, e.X, Convert.ToInt32(toolStripTextBox2.Text) * 2, Convert.ToInt32(toolStripTextBox2.Text));
                canPaint = false;
                drawRect = false;
            }
            else if (drawCircle)
            {
                SolidBrush s = new SolidBrush(toolStripButton1.ForeColor);
                gr.FillEllipse(s, e.X, e.X, Convert.ToInt32(toolStripTextBox2.Text) * 2, Convert.ToInt32(toolStripTextBox2.Text));
                canPaint = false;
                drawCircle = false;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canPaint = false;
            prev_X = null;
            prev_Y = null;
        }

        int? prev_X = null;
        int? prev_Y = null;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (canPaint)
                {
                    /* SolidBrush s = new SolidBrush(Color.Black);
                    gr.FillEllipse(s,e.X,e.Y,Convert.ToInt32( toolStripTextBox1.Text ),Convert.ToInt32( toolStripTextBox1.Text )); */

                    Pen p = new Pen(toolStripButton1.ForeColor, float.Parse(toolStripTextBox1.Text));
                    gr.DrawLine(p, new Point(prev_X ?? e.X, prev_Y ?? e.Y), new Point(e.X, e.Y));
                    prev_X = e.X;
                    prev_Y = e.Y;
                }
            }
            
            catch {MessageBox.Show("Set Pen Size");};
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog cd_obj = new ColorDialog();
            if (cd_obj.ShowDialog() == DialogResult.OK)
            {
                toolStripButton1.ForeColor = cd_obj.Color;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            gr.Clear(panel1.BackColor);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ColorDialog cd_obj = new ColorDialog();
            if (cd_obj.ShowDialog() == DialogResult.OK)
            {
                toolStripButton3.ForeColor = cd_obj.Color;
                panel1.BackColor = cd_obj.Color;
            }
        }
        bool drawSquare = false;
        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }
        bool drawRect = false;
        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawRect = true;
        }
        bool drawCircle = false;
        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCircle = true;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] imagePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in imagePaths)
            {
                gr.DrawImage(Image.FromFile(path),new Point(0,0));
            }
        }
    }
}
