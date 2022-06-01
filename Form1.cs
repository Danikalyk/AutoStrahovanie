using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AutoStrahovanie
{
    public partial class Form1 : Form
    {
        public string connectionString = @"Data Source=DESKTOP-A6KEL66\SQLEXPRESS;Initial Catalog=strahovanie;User ID=sa;Password=EmmojoyProoo123";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlExpression = $"SELECT * FROM auth where worker_login = '{textBox1.Text}' and worker_password = '{textBox2.Text}'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    reader.GetName(0);
                    int id = Convert.ToInt32(reader.GetValue(0));
                    D.id = id;
                    Form2 f = new Form2();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Введён неверный логин или пароль!");
                }
                reader.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
