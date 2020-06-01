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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();

            ToolTip One = new ToolTip();
            One.SetToolTip(button1, "Вернуться в главное меню!");
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
