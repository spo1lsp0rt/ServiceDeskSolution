using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceDesk
{
    public partial class RequestForm : Form
    {
        private readonly RequestHandler _handler;

        public RequestForm()
        {
            InitializeComponent();
            _handler = new RequestHandler();
        }

        private void send_form()
        {
            string name = textBox1.Text;
            string email = textBox3.Text;
            string title = textBox4.Text;
            string content = textBox2.Text;
            _handler.form_handler(name, email, title, content);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            send_form();
            MessageBox.Show(
                _handler.message,
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            if (_handler.result)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Затемнение родительской формы и уведение в инактив
            // take a screenshot of the form and darken it:
            Bitmap bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.CopyFromScreen(this.PointToScreen(new Point(0, 0)), new Point(0, 0), this.ClientRectangle.Size);
                double percent = 0.60;
                Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                using (Brush brsh = new SolidBrush(darken))
                {
                    G.FillRectangle(brsh, this.ClientRectangle);
                }
            }

            // put the darkened screenshot into a Panel and bring it to the front:
            using (Panel p = new Panel())
            {
                p.Location = new Point(0, 0);
                p.Size = this.ClientRectangle.Size;
                p.BackgroundImage = bmp;
                this.Controls.Add(p);
                p.BringToFront();

                Form2 window = new Form2();
                window.Owner = this;
                window.StartPosition = FormStartPosition.CenterParent;
                this.Visible = false;
                window.ShowDialog();
            }
        }
    }
}
