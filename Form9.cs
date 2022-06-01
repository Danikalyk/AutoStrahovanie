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
    public partial class Form9 : Form
    {
        public int prod;
        int coeff;
        public int cost;
        int cost1;
        int cost2;
        int cost3;
        public int platezh;
        public Form9()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        

        private void Form9_Load(object sender, EventArgs e)
        {
            Form1 f = new Form1();

            Get_Client();

            SqlConnection connRC = new SqlConnection(f.connectionString);
            string command = "SELECT tariff_name FROM tariffs";
            SqlDataAdapter da = new SqlDataAdapter(command, connRC);

            DataSet ds = new DataSet();
            connRC.Open();
            da.Fill(ds);
            connRC.Close();

            comboBox2.DataSource = ds.Tables[0];
            comboBox2.DisplayMember = "tariff_name";
            comboBox2.Text = " ";


            SqlConnection connRC2 = new SqlConnection(f.connectionString);
            string command2 = "SELECT dop_name from dop_pred";
            SqlDataAdapter da2 = new SqlDataAdapter(command2, connRC2);

            DataSet ds2 = new DataSet();
            connRC.Open();
            da2.Fill(ds2);
            connRC.Close();

            comboBox1.DataSource = ds2.Tables[0];
            comboBox1.DisplayMember = "dop_name";
            comboBox1.Text = " ";


            SqlConnection connRC3 = new SqlConnection(f.connectionString);
            string command3 = "SELECT dop_name from dop_pred";
            SqlDataAdapter da3 = new SqlDataAdapter(command3, connRC3);

            DataSet ds3 = new DataSet();
            connRC.Open();
            da3.Fill(ds3);
            connRC.Close();

            comboBox3.DataSource = ds3.Tables[0];
            comboBox3.DisplayMember = "dop_name";
            comboBox3.Text = " ";


            SqlConnection connRC4 = new SqlConnection(f.connectionString);
            string command4 = "SELECT dop_name from dop_pred";
            SqlDataAdapter da4 = new SqlDataAdapter(command4, connRC4);

            DataSet ds4 = new DataSet();
            connRC.Open();
            da4.Fill(ds4);
            connRC.Close();

            comboBox4.DataSource = ds4.Tables[0];
            comboBox4.DisplayMember = "dop_name";
            comboBox4.Text = " ";

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            string sqlExpression = $"SELECT dop_cost from dop_pred where dop_name = '{comboBox1.Text}'";
            using (SqlConnection connection = new SqlConnection(f.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    cost1 = Convert.ToInt32(reader.GetValue(0));
                    label2.Text = Convert.ToString(cost1);
                }
                reader.Close();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            string sqlExpression = $"SELECT dop_cost from dop_pred where dop_name = '{comboBox2.Text}'";
            using (SqlConnection connection = new SqlConnection(f.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    cost2 = Convert.ToInt32(reader.GetValue(0));
                    label3.Text = Convert.ToString(cost2);
                }
                reader.Close();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            string sqlExpression = $"SELECT dop_cost from dop_pred where dop_name = '{comboBox3.Text}'";
            using (SqlConnection connection = new SqlConnection(f.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    cost3 = Convert.ToInt32(reader.GetValue(0));
                    label4.Text = Convert.ToString(cost3);
                }
                reader.Close();
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            string sqlExpression = $"select tariff_prodolzhitelnost, tariff_coeff from tariffs where tariff_name = '{comboBox2.Text}'";
            using (SqlConnection connection = new SqlConnection(f.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    prod = Convert.ToInt32(reader.GetValue(0));
                    coeff = Convert.ToInt32(reader.GetValue(1));
                }
                reader.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            platezh = cost / prod / coeff + cost1 + cost2 + cost3;

            label6.Text = Convert.ToString(platezh);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex; // взяли строку с dataGridView1
            int ID = Convert.ToInt32(dataGridView1[0, row].Value);

            string command = $"select automobile.auto_id, client.client_surname, client.client_name, automobile.auto_gosnumber, automobile.auto_cost, automobile.auto_marka, automobile.auto_model, automobile.auto_createdate from client, automobile where automobile.client_id = client.client_id and automobile.client_id = {ID}";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Авто");
            dataGridView2.DataSource = ds.Tables["Авто"].DefaultView;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int row2 = dataGridView2.CurrentCell.RowIndex; // взяли строку с dataGridView1
            cost = Convert.ToInt32(dataGridView2[4, row2].Value);

            label7.Text = Convert.ToString(cost);
        }
    }
}
