using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Reflection;

namespace Truancy
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            ToolTip One = new ToolTip();
            One.SetToolTip(button9, "Добавление записи!");
            ToolTip Two = new ToolTip();
            Two.SetToolTip(button6, "Очистка заполненных полей!");
            ToolTip Three = new ToolTip();
            Three.SetToolTip(button4, "Изменение сохранённой ранее записи!");
            ToolTip Four = new ToolTip();
            Four.SetToolTip(button5, "Поиск по ключевому слову!");
            ToolTip Five = new ToolTip();
            Five.SetToolTip(button7, "Сформированный файл Excel находится в корневой папке программы!");
            ToolTip Six = new ToolTip();
            Six.SetToolTip(button3, "Сортировка по выбранному параметру!");
            ToolTip Seven = new ToolTip();
            Seven.SetToolTip(button8, "Суммирование всего времени пропусков!");
            ToolTip Eight = new ToolTip();
            Eight.SetToolTip(button2, "Удаление выбранной записи!");
            ToolTip Nine = new ToolTip();
            Nine.SetToolTip(button1, "Вернуться на главное окно!");
        }

        DataSet First = new DataSet();
        DataSet Second = new DataSet();
        DataSet Third = new DataSet();
        DataSet Fourth = new DataSet();

        private void Preload()
        {
            First = new DataSet();
            Second = new DataSet();
            Third = new DataSet();
            Fourth = new DataSet();

            First = new DataSet();
            Second = new DataSet();
            dataGridView1.DataSource = null;
            OleDbConnection D3 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Data.mdb");
            OleDbCommand D4 = new OleDbCommand("SELECT [Id пропуска],[Кол-во часов],[Студент],[Староста],[Группа],[Дата пропуска], [Причина]  FROM Progul;");
            OleDbDataAdapter D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(First);
            dataGridView1.DataSource = First.Tables[0];

            //СПИСОК 1
            D4 = new OleDbCommand("SELECT * FROM Starosty ;");
            D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(Second);

            comboBox1.Items.Clear();

            for (int i = 0; i < Second.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(Second.Tables[0].Rows[i][1].ToString());

            }

            //СПИСОК 2
            comboBox2.Items.Clear();

            D4 = new OleDbCommand("SELECT * FROM  Students ;");
            D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(Third);

            for (int i = 0; i < Third.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(Third.Tables[0].Rows[i][1].ToString());

            }

            //СПИСОК 3
            comboBox7.Items.Clear();

            D4 = new OleDbCommand("SELECT * FROM  Groups ;");
            D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(Fourth);

            for (int i = 0; i < Fourth.Tables[0].Rows.Count; i++)
            {
                comboBox7.Items.Add(Fourth.Tables[0].Rows[i][1].ToString());

            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            Preload();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                        }
            }
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                comboBox5.Text = dataGridView1[1, dataGridView1.SelectedRows[0].Index].Value.ToString();
            }
            catch { }
            try
            {
                comboBox2.Text = dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value.ToString();
            }
            catch { }
            try
            {
                comboBox1.Text = dataGridView1[3, dataGridView1.SelectedRows[0].Index].Value.ToString();
            }
            catch { }
            try
            {
                comboBox7.Text = dataGridView1[4, dataGridView1.SelectedRows[0].Index].Value.ToString();
            }
            catch { }
            try
            {
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1[5, dataGridView1.SelectedRows[0].Index].Value.ToString());
            }
            catch { }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Index == -1)
            { MessageBox.Show("Произошла ошибка"); }
            else
            {
                OleDbConnection DBcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Data.mdb");
                DBcon.Open();
                string queryString = "delete FROM Progul WHERE [Id пропуска] = @ФИО";
                OleDbCommand command = new OleDbCommand(queryString, DBcon);
                command.Parameters.AddWithValue("@ФИО", First.Tables[0].Rows[dataGridView1.SelectedRows[0].Index][0].ToString());
                command.ExecuteNonQuery();
                DBcon.Close();
                MessageBox.Show("Пропуск удалён!");
            }
            Preload();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            string W = "";

            if (checkBox1.Checked)
            {
                W = "Уважительная";
            }
            else
            {
                W = "Неуважительная";
            }
            OleDbConnection D3 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Data.mdb");
            OleDbCommand D4 = new OleDbCommand("INSERT INTO Progul( [Кол-во часов],[Студент],[Староста],[Группа],[Дата пропуска], [Причина] ) VALUES ('" + comboBox5.Text + "','" + comboBox2.Text + "','" + comboBox1.Text + "','" + comboBox7.Text + "','" + dateTimePicker1.Text + "','" + W + "')", D3);
            D3.Open();
            D4.ExecuteNonQuery();
            D3.Close();
            MessageBox.Show("Пропуск добавлен!");

            Preload();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows[0].Index == -1)
                { MessageBox.Show("Произошла ошибка"); }

                string W = "";
                if (checkBox1.Checked)
                {
                    W = "Уважительная";
                }
                else
                {
                    W = "Неуважительная";
                }
                OleDbConnection DBcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Data.mdb");
                DBcon.Open();
                string queryString = "UPDATE Progul SET  Progul.[Кол-во часов]  = '" + comboBox5.Text + "', Progul.Студент = '" + comboBox2.Text + "',Progul.Староста = '" + comboBox1.Text + "',Progul.[Группа] = '" + comboBox7.Text + "',Progul.[Дата пропуска] = '" + dateTimePicker1.Text + "', Progul.[Причина] = '" + W + "' WHERE [Id пропуска]=" + First.Tables[0].Rows[dataGridView1.SelectedRows[0].Index][0].ToString() + ";";
                OleDbCommand command = new OleDbCommand(queryString, DBcon);
                command.ExecuteNonQuery();
                DBcon.Close();
                MessageBox.Show("Пропуск успешно добавлен");
            }
            catch { }

            Preload();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Уважительная";
            }
            else
            {
                checkBox1.Text = "Неуважительная";
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            First = null;
            First = new DataSet();
            OleDbConnection D3 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Data.mdb");
            OleDbCommand D4 = new OleDbCommand("SELECT [Id пропуска],[Кол-во часов],[Студент],[Староста],[Группа],[Дата пропуска], [Причина]  FROM Progul;");
            OleDbDataAdapter D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(First);
            dataGridView1.DataSource = First.Tables[0];
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            First = null;
            First = new DataSet();
            OleDbConnection D3 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Data.mdb");
            OleDbCommand D4 = new OleDbCommand("SELECT [Id пропуска],[Кол-во часов],[Студент],[Староста],[Группа],[Дата пропуска], [Причина]  FROM Progul WHERE [Причина]= 'Уважительная';");
            OleDbDataAdapter D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(First);
            dataGridView1.DataSource = First.Tables[0];
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            First = null;
            First = new DataSet();
            OleDbConnection D3 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Data.mdb");
            OleDbCommand D4 = new OleDbCommand("SELECT [Id пропуска],[Кол-во часов],[Студент],[Староста],[Группа],[Дата пропуска], [Причина]  FROM Progul WHERE [Причина]= 'Неуважительная';");
            OleDbDataAdapter D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(First);
            dataGridView1.DataSource = First.Tables[0];
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            comboBox5.Text = null;
            comboBox2.Text = null;
            comboBox1.Text = null;
            comboBox7.Text = null;
            dateTimePicker1.Text = "";
            checkBox1.Checked = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "")
            {
                MessageBox.Show("Вы не выбрали критерий сортировки таблицы!");
            }
            else
            {
                dataGridView1.Sort(dataGridView1.Columns[comboBox3.Text], ListSortDirection.Ascending);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            int sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            }
            textBox2.Text = sum.ToString() + " Ч. Н.Б";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SaveTable(dataGridView1);
        }

        void SaveTable(DataGridView Save_Students)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Пропуски студентов";

            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int i = 1; i < dataGridView1.RowCount + 1; i++)
            {
                for (int j = 1; j < dataGridView1.ColumnCount + 1; j++)
                {
                    worksheet.Rows[i].Columns[j] = dataGridView1.Rows[i - 1].Cells[j - 1].Value;
                }
            }
            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(path);
            excelapp.Quit();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
