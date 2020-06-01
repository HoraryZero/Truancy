using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Truancy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ToolTip One = new ToolTip();
            One.SetToolTip(button1, "Просмотр и добавление пропуска студента!");
            ToolTip Two = new ToolTip();
            Two.SetToolTip(button2, "Просмотр и добавление студента!");
            ToolTip Three = new ToolTip();
            Three.SetToolTip(button6, "Просмотр и добавление старосты!");
            ToolTip Four = new ToolTip();
            Four.SetToolTip(button3, "Просмотр и добавление группы!");
            ToolTip Five = new ToolTip();
            Five.SetToolTip(button7, "Просмотр информации о программе!");
            ToolTip Six = new ToolTip();
            Six.SetToolTip(button4, "Выход из программы!");
            ToolTip Seven = new ToolTip();
            Seven.SetToolTip(button5, "Открыть помощь!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Help.chm");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }
    }
}
