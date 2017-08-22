using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TractionCalc
{
    public partial class Close : Form
    {
        public Close()
        {
            InitializeComponent();
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.DarkGreen;
        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Green;
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            InputForm inf = new InputForm();
            inf.Enabled = true;
            this.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
