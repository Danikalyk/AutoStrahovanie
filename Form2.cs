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
using System.IO;

namespace AutoStrahovanie
{
    public partial class Form2 : Form
    {
        int dop1;

        int dop2;

        int dop3;

        int status;

        object dt;

        int tariff;

        int count;

        int viplacheno;

        string path = "dogovor.txt";
        string text = "";

        public void My_Execute_Non_Query(string CommandText)
        {
            Form1 f = new Form1();
            SqlConnection conn = new SqlConnection(f.connectionString);
            conn.Open();
            SqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = CommandText;
            myCommand.ExecuteNonQuery();
            conn.Close();
        }
        //1 - Автомобили
        //2 - Клиенты
        //3 - Тарифы
        //4 - Документация
        //5 - Дополниткльные услуги
        public int act_table;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label6.Text = Convert.ToString(D.id);

            Get_Auto();
            Opening();
            Form1 f = new Form1();
            SqlConnection connRC = new SqlConnection(f.connectionString);
            string command2 = "SELECT status_name FROM dogovor_status";
            SqlDataAdapter da2 = new SqlDataAdapter(command2, connRC);

            DataSet ds2 = new DataSet();
            connRC.Open();
            da2.Fill(ds2);
            connRC.Close();

            comboBox2.DataSource = ds2.Tables[0];
            comboBox2.DisplayMember = "status_name";
            comboBox2.Text = " ";
        }

        private void Opening()
        {
            string command = "select dogovor.dogovor_id, worker.worker_surname, worker.worker_name, client.client_surname, client.client_name, tariffs.tariff_name, dogovor.viplata, dogovor.ezhemes_platezh, dogovor.createdate, dogovor.okonchaniedate, dogovor_status.status_name from dogovor, worker, client, tariffs, dogovor_status where dogovor.worker_id = worker.worker and dogovor.dogovor_status = dogovor_status.status_id and dogovor.client_id = client.client_id and dogovor.tariff = tariffs.tariff_id";
            if (!String.IsNullOrWhiteSpace(comboBox1.Text))
            {
                if (!String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    switch (comboBox1.Text)
                    {
                        case "Сотрудники":
                            command += $" and worker.worker_surname = '%{textBox1.Text}%'";
                            break;
                        case "Клиенты":
                            command += $" and client.client_surname = '%{textBox1.Text}%';";
                            break;
                        case "Марка машины":
                            command += $" and automobile.auto_marka = '%{textBox1.Text}%'";
                            break;
                    }
                }
            }
            if (!String.IsNullOrWhiteSpace(comboBox2.Text))
            {
                command += $" and dogovor_status.status_name = '{comboBox2.Text}'";
            }

            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Договоры");
            dataGridView1.DataSource = ds.Tables["Договоры"].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date1 = Convert.ToString(dateTimePicker1.Value);
            string date2 = Convert.ToString(dateTimePicker2.Value);

            string command = $"select worker.worker_surname, worker.worker_name, client.client_name, client.client_surname, automobile.auto_gosnumber, automobile.auto_marka, automobile.auto_model, tariffs.tariff_name, dop_pred.dop_name, dop_pred.dop_name, dop_pred.dop_name, dogovor.ezhemes_platezh, dogovor.createdate, dogovor.okonchaniedate, dogovor_status.status_name from dogovor, dogovor_status, tariffs, client, worker, automobile, dop_pred where dogovor.dogovor_status = dogovor_status.status_id and dogovor.worker_id = worker.worker and dogovor.client_id = client.client_id and dogovor.tariff = tariffs.tariff_id and dogovor.automobile_id =  automobile.auto_id and dogovor.dop_pred_1 = dop_pred.dop_id and dogovor.dop_pred_2 = dop_pred.dop_id and dogovor.dop_pred_3 = dop_pred.dop_id and dogovor.createdate between '{date1}' and '{date2}'";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Договоры");
            dataGridView1.DataSource = ds.Tables["Договоры"].DefaultView;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Opening();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Opening();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Opening();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            act_table = 1;
            Get_Auto();
        }

        private void Get_Auto()
        {
            string command = "select automobile.auto_id, client.client_surname, client.client_name, automobile.auto_gosnumber, automobile.auto_cost, automobile.auto_marka, automobile.auto_model, automobile.auto_createdate from client, automobile where automobile.client_id = client.client_id";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Авто");
            dataGridView2.DataSource = ds.Tables["Авто"].DefaultView;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            act_table = 2;
            Get_Client();
        }

        private void Get_Client()
        {
            string command = "select * from client";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Клиенты");
            dataGridView2.DataSource = ds.Tables["Клиенты"].DefaultView;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            act_table = 3;
            Get_Tariffs();
        }

        private void Get_Tariffs()
        {
            string command = "select * from tariffs";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Тарифы");
            dataGridView2.DataSource = ds.Tables["Тарифы"].DefaultView;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            act_table = 4;
            Get_Doc();
        }

        private void Get_Doc()
        {
            string command = "select automobile.auto_marka, automobile.auto_model, automobile.auto_gosnumber, autodocumentation.auto_prava, autodocumentation.auto_to, autodocumentation.auto_doverennost from autodocumentation, automobile where automobile.auto_id = autodocumentation.auto_id";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Документация");
            dataGridView2.DataSource = ds.Tables["Документация"].DefaultView;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            act_table = 5;
            Get_Dop();
        }

        private void Get_Dop()
        {
            string command = "select * from dop_pred where dop_name != ' '";
            Form1 f = new Form1();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Дополнительно");
            dataGridView2.DataSource = ds.Tables["Дополнительно"].DefaultView;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                switch (act_table)
                {
                    case 1:
                        Form4 f4 = new Form4();
                        if (f4.ShowDialog() == DialogResult.OK)
                        {
                            int row = f4.dataGridView1.CurrentCell.RowIndex; // взяли строку с dataGridView1
                            int ID = Convert.ToInt32(f4.dataGridView1[0, row].Value);
                            Add_Auto(ID, Convert.ToInt32(f4.textBox1.Text), Convert.ToInt32(f4.textBox2.Text), f4.textBox3.Text, f4.textBox4.Text, Convert.ToString(f4.dateTimePicker1.Value));
                            Get_Auto();
                        }
                        break;
                    case 2:
                        Form5 f5 = new Form5();
                        if (f5.ShowDialog() == DialogResult.OK)
                        {
                            Add_Client(f5.textBox1.Text, f5.textBox2.Text, f5.textBox3.Text, f5.textBox4.Text, f5.textBox5.Text);
                            Get_Client();
                        }
                        break;
                    case 3:
                        Form6 f6 = new Form6();
                        if (f6.ShowDialog() == DialogResult.OK)
                        {
                            Add_Tariff(f6.textBox1.Text, Convert.ToInt32(f6.numericUpDown1.Value), Convert.ToInt32(f6.numericUpDown2.Value));
                            Get_Tariffs();
                        }
                        break;
                    case 4:
                        Form7 f7 = new Form7();
                        if (f7.ShowDialog() == DialogResult.OK)
                        {
                            int row = f7.dataGridView1.CurrentCell.RowIndex; // взяли строку с dataGridView1
                            int ID = Convert.ToInt32(f7.dataGridView1[0, row].Value);
                            Add_Doc(ID, Convert.ToInt32(f7.maskedTextBox1.Text), Convert.ToInt32(f7.maskedTextBox2.Text), Convert.ToInt32(f7.maskedTextBox3.Text));
                            Get_Doc();
                        }
                        break;
                    case 5:
                        Form8 f8 = new Form8();
                        if (f8.ShowDialog() == DialogResult.OK)
                        {
                            Add_Dop(f8.textBox1.Text, Convert.ToInt32(f8.maskedTextBox1.Text));
                            Get_Dop();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex),"Что-то пошло не так!");
            }
        }

        private void Add_Auto(int id, int gos, int cost, string marka, string model, string createdate)
        {
            string command = $"insert into automobile (client_id, auto_gosnumber, auto_cost, auto_marka, auto_model, auto_createdate) values ({id}, {gos}, {cost}, '{marka}', '{model}', '{createdate}')";
            My_Execute_Non_Query(command);
        }

        private void Add_Client(string name, string surname, string otch, string adress, string phone)
        {
            string command = $"insert into client (client_name, client_surname, client_otchestvo, client_adress, client_phone) values ('{name}', '{surname}', '{otch}', '{adress}', {phone})";
            My_Execute_Non_Query(command);
        }

        private void Add_Tariff(string name, int coef, int mes)
        {
            string command = $"insert into tariffs (tariff_name, tariff_coeff, tariff_prodolzhitelnost) values ('{name}', {coef}, {mes})";
            My_Execute_Non_Query(command);
        }

        private void Add_Doc(int id, int prava, int to, int dov)
        {
            string command = $"insert into autodocumentation (auto_id, auto_prava, auto_to, auto_doverennost) values ({id}, {prava}, {to}, {dov})";
            My_Execute_Non_Query(command);
        }

        private void Add_Dop(string name, int stoim)
        {
            string command = $"insert into dop_pred (dop_name, dop_cost) values ('{name}', {stoim})";
            My_Execute_Non_Query(command);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            int row = dataGridView2.CurrentCell.RowIndex; // взяли строку с dataGridView1
            int ID = Convert.ToInt32(dataGridView2[0, row].Value);
            try
            {
                switch (act_table)
                {
                    case 1:
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            string command = $"delete from automobile where auto_id = " + ID;
                            My_Execute_Non_Query(command);
                            Get_Auto();
                        }
                        break;
                    case 2:
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            string command = $"delete from client where client_id = " + ID;
                            My_Execute_Non_Query(command);
                            Get_Client();
                        }
                        break;
                    case 3:
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            string command = $"delete from tariffs where tariff_id = " + ID;
                            My_Execute_Non_Query(command);
                            Get_Tariffs();
                        }
                        break;
                    case 4:
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            string command = $"delete from autodocumentation where auto_id = " + ID;
                            My_Execute_Non_Query(command);
                            Get_Doc();
                        }
                        break;
                    case 5:
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            string command = $"delete from dop_pred where dop_id = " + ID;
                            My_Execute_Non_Query(command);
                            Get_Dop();
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Возникла проблема при удалении элемента, возможно он используется в другой таблице!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            string today_date = Convert.ToString(dateTimePicker3.Value);
            string command = $"select worker.worker_surname, worker.worker_name, client.client_name, client.client_surname, automobile.auto_gosnumber, automobile.auto_marka, automobile.auto_model, tariffs.tariff_name, dop_pred.dop_name, dop_pred.dop_name, dop_pred.dop_name, dogovor.ezhemes_platezh, dogovor.createdate, dogovor.okonchaniedate, dogovor_status.status_name from dogovor, dogovor_status, tariffs, client, worker, automobile, dop_pred where dogovor.dogovor_status = dogovor_status.status_id and dogovor.worker_id = worker.worker and dogovor.client_id = client.client_id and dogovor.tariff = tariffs.tariff_id and dogovor.automobile_id =  automobile.auto_id and dogovor.dop_pred_1 = dop_pred.dop_id and dogovor.dop_pred_2 = dop_pred.dop_id and dogovor.dop_pred_3 = dop_pred.dop_id and datediff(day, dogovor.okonchaniedate, getdate()) <= 14";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Окончание");
            dataGridView1.DataSource = ds.Tables["Окончание"].DefaultView;

            MessageBox.Show("Предупреждения разосланы!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            string sqlExpression4 = $"select status_id from dogovor_status where status_name = 'Истёк'";
            using (SqlConnection connection = new SqlConnection(f1.connectionString))
            {
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlExpression4, connection);
                SqlDataReader reader = command2.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    status = Convert.ToInt32(reader.GetValue(0));
                }
                reader.Close();
            }
            string command = $"update dogovor set dogovor_status = {status} where datediff(day, dogovor.okonchaniedate, getdate()) <= 0";
            My_Execute_Non_Query(command);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Form9 f = new Form9();

            try
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    int row = f.dataGridView1.CurrentCell.RowIndex; // взяли строку с dataGridView1
                    int ID = Convert.ToInt32(f.dataGridView1[0, row].Value);

                    int row2 = f.dataGridView2.CurrentCell.RowIndex; // взяли строку с dataGridView1
                    int ID2 = Convert.ToInt32(f.dataGridView2[0, row2].Value);


                    Form1 f1 = new Form1();

                    dt = DateTime.Today.AddMonths(f.prod).Date;

                    string sqlExpression = $"select dop_id from dop_pred where dop_name = '{f.comboBox1.Text}'";
                    using (SqlConnection connection = new SqlConnection(f1.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            dop1 = Convert.ToInt32(reader.GetValue(0));
                        }
                        reader.Close();
                    }
                    string sqlExpression2 = $"select dop_id from dop_pred where dop_name = '{f.comboBox3.Text}'";
                    using (SqlConnection connection = new SqlConnection(f1.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression2, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            dop2 = Convert.ToInt32(reader.GetValue(0));
                        }
                        reader.Close();
                    }
                    string sqlExpression3 = $"select dop_id from dop_pred where dop_name = '{f.comboBox4.Text}'";
                    using (SqlConnection connection = new SqlConnection(f1.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression3, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            dop3 = Convert.ToInt32(reader.GetValue(0));
                        }
                        reader.Close();
                    }
                    string sqlExpression4 = $"select status_id from dogovor_status where status_name = 'Действует'";
                    using (SqlConnection connection = new SqlConnection(f1.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression4, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            status = Convert.ToInt32(reader.GetValue(0));
                        }
                        reader.Close();
                    }
                    string sqlExpression5 = $"select tariff_id from tariffs where tariff_name = '{f.comboBox2.Text}'";
                    using (SqlConnection connection = new SqlConnection(f1.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression5, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            tariff = Convert.ToInt32(reader.GetValue(0));
                        }
                        reader.Close();
                    }
                    text = $"Номер сотрудника: {label6.Text}\nНомер клиента: {ID}\nНомер авто: {Convert.ToString(ID2)}\nДоп.услуга 1: {dop1}\nДоп.услуга 2: {dop3}\nДоп.услуга 3: {dop3}\nПлатёж: {f.platezh}\nДата заключения договора: {Convert.ToString(dateTimePicker4.Value)}\nДата окончания действия договора: {Convert.ToString(dt)}\nВыплата при наступлении страхового случая: {f.cost + (f.cost / 2)}";

                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        await writer.WriteLineAsync(text);
                    }
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    // получаем выбранный файл
                    string filename = saveFileDialog1.FileName;
                    // сохраняем текст в файл
                    System.IO.File.WriteAllText(filename, text);
                    Add_Dogovor(Convert.ToInt32(label6.Text), ID, ID2, tariff, dop1, dop2, dop3, f.platezh, Convert.ToString(dateTimePicker4.Value), Convert.ToString(dt), f.cost + (f.cost / 2), status);
                    Opening();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при добавлении записи, возможно не все поля были заполнены!");
            }
        }

        private void Add_Dogovor(int worker, int client, int tariff, int auto, int dop1, int dop2, int dop3, int platezh, string createdate, string okonchanie, int viplata, int status)
        {
            string command = $"insert into dogovor (worker_id, client_id, automobile_id, tariff, dop_pred_1, dop_pred_2, dop_pred_3, ezhemes_platezh, createdate, okonchaniedate, viplata, dogovor_status) values ({worker}, {client}, {auto}, {tariff}, {dop1}, {dop2}, {dop3}, {platezh}, '{createdate}', '{okonchanie}', {viplata}, {status})";
            My_Execute_Non_Query(command);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            string sqlExpression4 = $"select status_id from dogovor_status where status_name = 'Произведена выплата'";
            using (SqlConnection connection = new SqlConnection(f1.connectionString))
            {
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlExpression4, connection);
                SqlDataReader reader = command2.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    status = Convert.ToInt32(reader.GetValue(0));
                }
                reader.Close();
            }

            int row = dataGridView1.CurrentCell.RowIndex; // взяли строку с dataGridView1
            int ID = Convert.ToInt32(dataGridView1[0, row].Value);

            string command = $"update dogovor set dogovor_status = {status} where dogovor_id = {ID}";
            My_Execute_Non_Query(command);
            Opening();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Opening();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();

            string sqlExpression4 = $"select status_id from dogovor_status where status_name = 'Произведена выплата'";
            using (SqlConnection connection = new SqlConnection(f1.connectionString))
            {
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlExpression4, connection);
                SqlDataReader reader = command2.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    status = Convert.ToInt32(reader.GetValue(0));
                }
                reader.Close();
            }
            string sqlExpression = $"select max(dogovor_id) from dogovor";
            using (SqlConnection connection = new SqlConnection(f1.connectionString))
            {
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command2.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    count = Convert.ToInt32(reader.GetValue(0));
                }
                reader.Close();
            }
            string sqlExpression2 = $"select viplata from dogovor where dogovor_status = {status}";
            using (SqlConnection connection = new SqlConnection(f1.connectionString))
            {
                connection.Open();
                SqlCommand command3 = new SqlCommand(sqlExpression2, connection);
                SqlDataReader reader = command3.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())
                    {
                        viplacheno += Convert.ToInt32(reader.GetValue(0));
                    }
                }
                else
                {
                    viplacheno = 0;
                }
                reader.Close();
            }
            text = $"Договоров заключено: {count}\nВыплачено: {viplacheno}";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(text);
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, text);
        }
    }
}
