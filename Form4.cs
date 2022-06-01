using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStrahovanie
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Get_Client();
        }

        private void Get_Client()
        {
            string command = "select * from client";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Клиенты");
            dataGridView1.DataSource = ds.Tables["Клиенты"].DefaultView;
        }
    }
}
