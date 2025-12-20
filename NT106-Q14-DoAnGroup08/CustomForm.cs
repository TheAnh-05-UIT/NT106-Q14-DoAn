using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NewNet_Manager
{
    public class CustomForm : Form
    {
        private int borderRadius = 20; // bán kính bo góc
        private int borderWidth = 1;   // độ dày viền
        private Color borderColor = Color.Black; // màu viền
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public CustomForm()
        {
            this.FormBorderStyle = FormBorderStyle.None; // bỏ viền mặc định
            this.BackColor = Color.White; // màu nền Form
            this.Padding = new Padding(borderWidth);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 300);

            // Sự kiện chuột để kéo Form
            this.MouseDown += MyForm_MouseDown;
            this.MouseMove += MyForm_MouseMove;
            this.MouseUp += MyForm_MouseUp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ viền tròn
            Rectangle rect = this.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using (GraphicsPath path = RoundedRect(rect, borderRadius))
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Region = new Region(RoundedRect(this.ClientRectangle, borderRadius));
            this.Invalidate(); // vẽ lại khi resize
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        // --- Kéo Form bằng chuột ---
        private void MyForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void MyForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void MyForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
