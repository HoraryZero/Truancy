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
    public partial class Form3 : Form
    {
        string Smart = "";

        public Form3()
        {
            InitializeComponent();

            ToolTip One = new ToolTip();
            One.SetToolTip(button2, "Добавление студента!");
            ToolTip Two = new ToolTip();
            Two.SetToolTip(button4, "Изменение сохранённого ранее студента!");
            ToolTip Three = new ToolTip();
            Three.SetToolTip(button1, "Удаление выбранного студента!");
            ToolTip Four = new ToolTip();
            Four.SetToolTip(button7, "Сформированный файл Excel находится в корневой папке программы!");
            ToolTip Five = new ToolTip();
            Five.SetToolTip(button3, "Поиск по ключевому слову");
            ToolTip Six = new ToolTip();
            Six.SetToolTip(button5, "Вернуться в главное меню!");
            Smart = maskedTextBox1.Text;
        }

        DataSet First = new DataSet();
        DataSet Second = new DataSet();

        private void Preload()
        {
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            Second = new DataSet();
            First = new DataSet();
            Second = new DataSet();
            dataGridView1.DataSource = null;
            OleDbConnection D3 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Data.mdb");
            OleDbCommand D4 = new OleDbCommand("SELECT * FROM Students;");
            OleDbDataAdapter D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(First);
            dataGridView1.DataSource = First.Tables[0];
            comboBox1.Items.Clear();
            D4 = new OleDbCommand("SELECT Groups.Название FROM Groups;");
            D5 = new OleDbDataAdapter(D4.CommandText, D3);
            D5.Fill(Second);
            comboBox1.Items.Clear();
            for (int i = 0; i < Second.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(Second.Tables[0].Rows[i][0].ToString());
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Preload();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox1.Text == "" || maskedTextBox1.Text == Smart)
            { MessageBox.Show("Произошла ошибка"); }
            else
            {
                OleDbConnection D3 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Data.mdb");
                OleDbCommand D4 = new OleDbCommand("INSERT INTO Students( ФИО, [Номер телефона], Группа ) VALUES ('" + textBox1.Text + "','" + maskedTextBox1.Text + "','" + comboBox1.Text + "')", D3);
                D3.Open();
                D4.ExecuteNonQuery();
                D3.Close();
                MessageBox.Show("Студент добавлен!");
            }
            Preload();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Index == -1)
            { MessageBox.Show("Произошла ошибка"); }
            else
            {
                OleDbConnection DBcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Data.mdb");
                DBcon.Open();
                string queryString = "delete FROM Students WHERE [ID Студента] = @ФИО";
                OleDbCommand command = new OleDbCommand(queryString, DBcon);
                command.Parameters.AddWithValue("@ФИО", First.Tables[0].Rows[dataGridView1.SelectedRows[0].Index][0].ToString());
                command.ExecuteNonQuery();
                DBcon.Close();
                MessageBox.Show("Cтудент удалён!");
            }
            Preload();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Index == -1)
            { MessageBox.Show("Произошла ошибка"); }
            else
            {
                OleDbConnection DBcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Data.mdb");
                DBcon.Open();
                string queryString = "UPDATE Students SET Students.ФИО = @ФИО, Students.[Номер телефона] = @Номертелефона, Students.Группа = @Группа WHERE [ID Студента]=" + First.Tables[0].Rows[dataGridView1.SelectedRows[0].Index][0].ToString() + ";";
                OleDbCommand command = new OleDbCommand(queryString, DBcon);
                command.Parameters.AddWithValue("@ФИО", textBox1.Text);
                command.Parameters.AddWithValue("@Номертелефона", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@Группа", comboBox1.Text);
                command.ExecuteNonQuery();
                DBcon.Close();
                MessageBox.Show("Cтудент добавлен!");
            }
            Preload();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox3.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                        }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SaveTable(dataGridView1);
        }

        void SaveTable(DataGridView Save_Students)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Список студентов";

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
    }
}
