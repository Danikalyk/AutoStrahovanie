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
    public partial class Form7 : Form
    {
        public Form7()
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

        private void Form7_Load(object sender, EventArgs e)
        {
            string command = "select automobile.auto_id, client.client_surname, client.client_name, automobile.auto_gosnumber, automobile.auto_marka, automobile.auto_model from automobile, client where automobile.client_id = client.client_id";
            string command2 = "select * from automobile";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command2, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Авто");
            dataGridView1.DataSource = ds.Tables["Авто"].DefaultView;
        }
    }
}
