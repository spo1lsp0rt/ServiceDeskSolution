using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ServiceDesk
{
    public partial class Form2 : Form
    {
        private readonly RequestHandler _handler;
        public Form2()
        {
            InitializeComponent();
            _handler = new RequestHandler();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            DataTable data = _handler.return_result(email);
            if (_handler.result)
            {
                textBox3.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                dataGridView1.Visible = true;
                dataGridView1.DataSource = data;
                dataGridView1.Columns["Ответ"].Visible = false;
                dataGridView1.Columns[0].Width = 150;
                dataGridView1.Columns[1].Width = 250;
                dataGridView1.Columns[2].Width = 100;
            }
            else
            {
                MessageBox.Show(
                    _handler.message,
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        public static bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email.ToLower(), pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(
                dataGridView1.SelectedRows[0].Cells[3].Value.ToString(),
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
